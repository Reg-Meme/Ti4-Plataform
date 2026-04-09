using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsGrab : Mechanics
{
    [Header("Physic Grab")]
    public Transform grabPoint;
    public Transform overheadPoint;

    Transform cameraTransform;
    PhysicsGrabConfig config;
    LayerMask interactionLayer;

    float maxLiftMass;


    public PhysicsGrab(Transform camera, LayerMask layer, PhysicsGrabConfig config, Transform grabPoint, Transform overheadPoint)
    {
        this.cameraTransform = camera;
        this.interactionLayer = layer;
        this.config = config;
        this.grabPoint = grabPoint;
        this.overheadPoint = overheadPoint;
    }

    [Header("Highlight")]

    public Material highlightMaterial;

    Renderer lastRenderer;
    Material originalMaterial;

    Rigidbody grabbedObject;
    bool holdingOverhead;

    public override void Tick()
    {
        CheckHighlight();

        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * config.grabDistance, Color.red);
    }
    public override void FixedTick()
    {
        if (grabbedObject != null)
        {
            MoveObject();
        }
    }

    public override void AttackButton()
    {
        if (grabbedObject != null)
        {
            ThrowObject();
            return;
        }
        if (!battery.Consume(batteryCost))
            return;

        RaycastHit hit;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, config.grabDistance, interactionLayer))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                float batteryFactor = battery.GetNormalized();
                float allowedMass = maxLiftMass * batteryFactor;

                if (rb.mass <= allowedMass)
                {
                    grabbedObject = rb;
                }
            }
        }
        Debug.Log("ATAQUE FOI CHAMADO");
    }

    public override void AimButton()
    {
        holdingOverhead = true;
    }
    public override void ReleaseAim()
    {
        holdingOverhead = false;
    }
    void MoveObject()
    {
        Transform targetPoint = holdingOverhead ? overheadPoint : grabPoint;

        float weightFactor = 1f / grabbedObject.mass;

        Vector3 dir = targetPoint.position - grabbedObject.position;
        grabbedObject.AddForce(dir * config.grabForce * weightFactor, ForceMode.Acceleration);
        grabbedObject.angularVelocity *= 0.95f;

        if (Vector3.Distance(targetPoint.position, grabbedObject.position) > 5f) Release();
        if (battery.currentBattery < 20f)
        {
            Vector3 shake = Random.insideUnitSphere * 0.1f;
            grabbedObject.AddForce(shake, ForceMode.Acceleration);
        }
    }

    void ThrowObject()
    {
        Vector3 throwDir = cameraTransform.forward;
        grabbedObject.AddForce(throwDir * config.throwForce * grabbedObject.mass, ForceMode.Impulse);

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

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, config.grabDistance, interactionLayer))
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