using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsGrab : Mechanics
{
    public Transform grabPoint;
    public Transform overheadPoint;

    Material highlightMaterial;
    Transform cameraTransform;
    PhysicsGrabConfig config;
    LayerMask interactionLayer;

    float maxLiftMass;
    float rotationInput;


    public PhysicsGrab(Transform camera, LayerMask layer, PhysicsGrabConfig config, Transform grabPoint, Transform overheadPoint, Material highlightMaterial)
    {
        this.cameraTransform = camera;
        this.interactionLayer = layer;
        this.maxLiftMass = config.maxLiftMass;
        this.config = config;
        this.grabPoint = grabPoint;
        this.overheadPoint = overheadPoint;
        this.highlightMaterial = highlightMaterial;
    }


    Renderer lastRenderer;
    Material[] originalMaterials;
    Rigidbody grabbedObject;
    bool holdingOverhead;

    public override void Tick()
    {
        CheckHighlight();
        rotationInput = Mouse.current.scroll.ReadValue().y;
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
        if(Moviment.moviment.BottleMode) return;
        if (grabbedObject != null)
        {
            ThrowObject();
            return;
        }
        // if (!battery.Consume(batteryCost))
        //     return;

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

        // for�a principal
        Vector3 force = dir * config.grabForce;

        // damping (anti-tremor)
        Vector3 dampingForce = -grabbedObject.linearVelocity * config.damping;

        grabbedObject.AddForce((force + dampingForce) * weightFactor, ForceMode.Acceleration);

        if (Vector3.Distance(targetPoint.position, grabbedObject.position) > 5f) Release();
        if (battery.currentBattery < 20f)
        {
            Vector3 shake = Random.insideUnitSphere * config.lowBatteryShake;
            grabbedObject.AddForce(shake, ForceMode.Acceleration);
        }
        if (Mathf.Abs(rotationInput) > 0.01f)
        {
            grabbedObject.AddTorque(cameraTransform.up * rotationInput * 5f, ForceMode.Acceleration);
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

                originalMaterials = r.sharedMaterials;
                r.sharedMaterial = highlightMaterial;

                lastRenderer = r;
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    void ClearHighlight()
    {
        if (lastRenderer != null)
        {
            lastRenderer.sharedMaterials = originalMaterials;
            lastRenderer = null;
        }
    }
}