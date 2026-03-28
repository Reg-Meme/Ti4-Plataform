using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsGrab : Mechanics
{
    [Header("Physic Grab")]
    public Transform grabPoint;
    public Transform overheadPoint;

    public float grabDistance = 3f;
    public float grabForce = 150f;
    public float throwForce = 8f;
    public float battery = 100f;
    public float batteryCost = 10f;
    public float overheadMassLimit = 5f;
    public float maxLiftMass = 10f;

    public LayerMask interactionLayer;

    public InputActionReference interact;
    public InputActionReference throwAction;

    [Header ("Highlight")]

    public Material highlightMaterial;

    Renderer lastRenderer;
    Material originalMaterial;

    Rigidbody grabbedObject;
    bool holdingOverhead;

    void Update()
    {
        CheckHighlight();
        if (interact.action.WasPressedThisFrame())
        {
            if (grabbedObject == null) Use();
            else Release();
        }

        if (grabbedObject != null && throwAction.action.WasPressedThisFrame())
        {
            ThrowObject();
        }
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * grabDistance, Color.red);
    }

    void FixedUpdate()
    {
        if (grabbedObject != null)
        {
            MoveObject();
        }
    }

    public override void Use()
    {
        if (battery <= 0) return;

        RaycastHit hit;

        if (Physics.Raycast(
            Camera.main.transform.position,
            Camera.main.transform.forward,
            out hit,
            grabDistance,
            interactionLayer))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                float batteryFactor = battery / 100f;
                float allowedMass = maxLiftMass * batteryFactor;

                if (rb.mass <= allowedMass)
                {
                    grabbedObject = rb;
                    battery -= 10f;
                }
            }
        }
    }

    void MoveObject()
    {
        Transform targetPoint = holdingOverhead ? overheadPoint : grabPoint;

        float weightFactor = 1f / grabbedObject.mass;

        Vector3 dir = targetPoint.position - grabbedObject.position;
        grabbedObject.AddForce(dir * grabForce * weightFactor, ForceMode.Acceleration);
        grabbedObject.angularVelocity *= 0.95f;

        if (Vector3.Distance(targetPoint.position, grabbedObject.position) > 5f)  Release();
        if (battery < 20f)
        {
            Vector3 shake = Random.insideUnitSphere * 0.1f;
            grabbedObject.AddForce(shake, ForceMode.Acceleration);
        }
    }

    void ThrowObject()
    {
        Vector3 throwDir = Camera.main.transform.forward;
        grabbedObject.AddForce(throwDir * throwForce * grabbedObject.mass, ForceMode.Impulse);

        Release();
    }

    void Release()
    {
        grabbedObject = null;
        holdingOverhead = false;
    }
    void CheckHighlight()
    {
        RaycastHit hit;

        if (Physics.Raycast(
            Camera.main.transform.position,
            Camera.main.transform.forward,
            out hit,
            grabDistance,
            interactionLayer))
        {
            Renderer r = hit.collider.GetComponent<Renderer>();

            if (r != null && r != lastRenderer)
            {
                ClearHighlight();

                originalMaterial = r.material;
                r.material = highlightMaterial;

                lastRenderer = r;
            }
        }
        else
        {
            ClearHighlight();
        }
        void ClearHighlight()
        {
            if (lastRenderer != null)
            {
                lastRenderer.material = originalMaterial;
                lastRenderer = null;
            }
        }
    }
}