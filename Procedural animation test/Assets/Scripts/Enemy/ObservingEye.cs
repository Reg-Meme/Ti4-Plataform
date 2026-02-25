using UnityEngine;
using System.Collections;
public class ObservingEye : MonoBehaviour
{
    public Transform anchorPoint;
    public float chainLength = 10f;
    public float patrolSpeed = 1.5f;
    public float returnSpeed = 2.0f;
    public float followSpeed = 0.5f;
    public float stopDistance = 3f;
    public float bodyRadius = 0.5f;
    public string playerTag = "Player";
    public float viewDistance = 15f;
    public float viewAngle = 60f;
    public float visionSphereRadius = 0.3f; 
    public LayerMask obstacleMask;
    public float detectionInterval = 0.2f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;
    public float preferredHeight = 2.0f;
    public float heightSmoothness = 5f;
    public LayerMask groundLayer;
    public bool isAlert = false;
    private bool returningToPatrol = false;
    private Transform targetTransform;
    private Quaternion initialRotation;
    private Vector3 startPosition;
    private Vector3 noiseOffset;
    private Vector3 currentVelocity;
    private float currentGroundY;
    private float chainLengthSqr;

    void Start()
    {
        initialRotation = transform.localRotation;
        startPosition = transform.position;
        chainLengthSqr = chainLength * chainLength;
        currentGroundY = transform.position.y;

        StartCoroutine(DetectionTick());
        StartCoroutine(UpdateNoise());
    }

    void Update()
    {
        HeightControl();

        if (isAlert) AlertMode();
        else if (returningToPatrol) PatrolReturn();
        else PatrolMode();
    }

    IEnumerator UpdateNoise()
    {
        while (true)
        {
            float time = Time.time * floatFrequency;
            noiseOffset = new Vector3(
                (Mathf.PerlinNoise(time, 0f) - 0.5f),
                (Mathf.PerlinNoise(0f, time) - 0.5f),
                (Mathf.PerlinNoise(time, time) - 0.5f)
            ) * floatAmplitude;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator DetectionTick()
    {
        WaitForSeconds wait = new WaitForSeconds(detectionInterval);
        while (true)
        {
            if (!isAlert) DetectPlayer();
            yield return wait;
        }
    }

    void SafeMoveTo(Vector3 targetPos, float speed, bool useSmoothDamp = false)
    {
        Vector3 currentPos = transform.position;
        Vector3 nextPos = useSmoothDamp 
            ? Vector3.SmoothDamp(currentPos, targetPos, ref currentVelocity, speed)
            : Vector3.Lerp(currentPos, targetPos, Time.deltaTime * speed);

        if (anchorPoint != null)
        {
            Vector3 offset = nextPos - anchorPoint.position;
            if (offset.sqrMagnitude > chainLengthSqr)
                nextPos = anchorPoint.position + offset.normalized * chainLength;
        }

        Vector3 moveDir = nextPos - currentPos;
        float moveDist = moveDir.magnitude;
        if (moveDist > 0.001f)
        {
            if (Physics.SphereCast(currentPos, bodyRadius, moveDir.normalized, out RaycastHit hit, moveDist, obstacleMask))
                nextPos = currentPos + moveDir.normalized * (hit.distance - 0.05f);
        }

        transform.position = nextPos;
    }

    void PatrolMode()
    {
        SafeMoveTo(startPosition + noiseOffset, patrolSpeed);
        float t = Time.time * patrolSpeed;
        Quaternion targetRot = initialRotation * Quaternion.Euler(Mathf.Sin(t) * 20f, Mathf.Sin(t * 0.8f) * 45f, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime);
    }

    void AlertMode()
    {
        if (targetTransform == null) return;

        Vector3 diff = targetTransform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-diff), Time.deltaTime * 5f);

        float distSqr = diff.sqrMagnitude;
        Vector3 targetPos = (distSqr <= stopDistance * stopDistance) ? transform.position : targetTransform.position + noiseOffset;

        SafeMoveTo(targetPos, followSpeed, true);

        if (distSqr > (viewDistance * 1.5f) * (viewDistance * 1.5f) || !HasLineOfSight(targetTransform))
        {
            isAlert = false;
            returningToPatrol = true;
            targetTransform = null;
        }
    }

    void PatrolReturn()
    {
        SafeMoveTo(startPosition + noiseOffset, returnSpeed, true);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, initialRotation, Time.deltaTime * returnSpeed);
        if ((transform.position - startPosition).sqrMagnitude < 1f) returningToPatrol = false;
    }

    void DetectPlayer()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, viewDistance);
        foreach (var potentialTarget in targets)
        {
            if (potentialTarget.CompareTag(playerTag))
            {
                Vector3 dir = (potentialTarget.transform.position - transform.position).normalized;
                if (Vector3.Angle(-transform.forward, dir) < viewAngle * 0.5f)
                {
                    if (HasLineOfSight(potentialTarget.transform))
                    {
                        targetTransform = potentialTarget.transform;
                        isAlert = true;
                        returningToPatrol = false;
                        break;
                    }
                }
            }
        }
    }

    bool HasLineOfSight(Transform target)
    {
        Vector3 origin = transform.position;
        Vector3 targetCenter = target.position + Vector3.up * 0.5f;
        Vector3 dir = (targetCenter - origin).normalized;
        float dist = Vector3.Distance(origin, targetCenter);

        return !Physics.SphereCast(origin, visionSphereRadius, dir, out _, dist, obstacleMask);
    }

    void HeightControl()
    {
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, 10f, groundLayer))
            currentGroundY = hit.point.y;
        
        float targetY = currentGroundY + preferredHeight + noiseOffset.y;
        Vector3 pos = transform.position;
        pos.y = Mathf.Lerp(pos.y, targetY, Time.deltaTime * heightSmoothness);
        transform.position = pos;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying && startPosition == Vector3.zero) startPosition = transform.position;

        // 1. Área de Visão
        Gizmos.color = isAlert ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        // 2. Cone de Visão
        Vector3 forwardDir = -transform.forward;
        Vector3 leftRay = Quaternion.AngleAxis(-viewAngle / 2, transform.up) * forwardDir;
        Vector3 rightRay = Quaternion.AngleAxis(viewAngle / 2, transform.up) * forwardDir;
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, leftRay * viewDistance);
        Gizmos.DrawRay(transform.position, rightRay * viewDistance);

        // 3. Raio Físico de Colisão
        Gizmos.color = new Color(0, 1, 1, 0.3f);
        Gizmos.DrawSphere(transform.position, bodyRadius);

        // 4. Âncora
        if (anchorPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(anchorPoint.position, chainLength);
            Gizmos.DrawLine(transform.position, anchorPoint.position);
        }
    }
}
