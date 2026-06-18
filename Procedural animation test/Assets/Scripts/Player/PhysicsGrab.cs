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

    Vector3 rotationAxis = Vector3.up;

    Renderer lastRenderer;
    Material[] originalMaterials;
    Rigidbody grabbedObject;
    bool holdingOverhead;

    public PhysicsGrab( Transform camera, LayerMask layer, PhysicsGrabConfig config,  Transform grabPoint, Transform overheadPoint, Material highlightMaterial)
    {
        this.cameraTransform = camera;
        this.interactionLayer = layer;
        this.maxLiftMass = config.maxLiftMass;
        this.config = config;
        this.grabPoint = grabPoint;
        this.overheadPoint = overheadPoint;
        this.highlightMaterial = highlightMaterial;
    }

    public override void Tick()
    {
        CheckHighlight();

        rotationInput = Mouse.current.scroll.ReadValue().y;

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            if (rotationAxis == Vector3.up) rotationAxis = Vector3.right;
            else if (rotationAxis == Vector3.right) rotationAxis = Vector3.forward;
            else rotationAxis = Vector3.up;

           // Debug.Log("Eixo de rotação: " + rotationAxis);
        }

        Debug.DrawRay( grabPoint.position,grabPoint.forward * config.grabDistance, Color.red);
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
        if (PlayerStats.bottleMode)
            return;

        if (grabbedObject != null)
        {
            Release();
            return;
        }

        RaycastHit hit;

        if (Physics.Raycast(grabPoint.position, grabPoint.forward,out hit,config.grabDistance, interactionLayer))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                float batteryFactor = battery.GetNormalized();
                float allowedMass = maxLiftMass * batteryFactor;

                if (rb.mass <= allowedMass)
                {
                    grabbedObject = rb;

                    grabbedObject.useGravity = false;
                    grabbedObject.linearDamping = 5f;
                    grabbedObject.interpolation = RigidbodyInterpolation.Interpolate;
                    grabbedObject.collisionDetectionMode =CollisionDetectionMode.Continuous;
                    grabbedObject.constraints =RigidbodyConstraints.FreezeRotation;
                }
            }
        }
    }

    public override void AimButton()
    {
        if (grabbedObject != null)
        {
            ThrowObject();return;
        }

        holdingOverhead = true;
    }

    public override void ReleaseAim()
    {
        holdingOverhead = false;
    }

    void MoveObject()
    {
        if (grabbedObject == null)return;

        PlayerStats.GrabMode = true;

        Transform targetPoint = holdingOverhead ? overheadPoint : grabPoint;

        float weightFactor = 1f / grabbedObject.mass;

        Vector3 dir = targetPoint.position - grabbedObject.position;

        Vector3 force = dir * config.grabForce;

        Vector3 dampingForce = -grabbedObject.linearVelocity * config.damping;

        grabbedObject.AddForce( (force + dampingForce) * weightFactor,ForceMode.Acceleration);

        if (Vector3.Distance(targetPoint.position,grabbedObject.position) > 5f)
        {
            Release();
        }

        if (battery.currentBattery < 20f)
        {
            Vector3 shake = Random.insideUnitSphere * config.lowBatteryShake;

            grabbedObject.AddForce( shake,ForceMode.Acceleration);
        }
        if (Mathf.Abs(rotationInput) > 0.01f)
        {
            grabbedObject.transform.Rotate(rotationAxis, rotationInput * 5f,Space.World);
        }
    }

    void ThrowObject()
    {
        if (grabbedObject == null) return;

        Vector3 throwDir = grabPoint.forward;

        grabbedObject.useGravity = true;
        grabbedObject.linearDamping = 0f;

        grabbedObject.constraints = RigidbodyConstraints.None;

        grabbedObject.AddForce( throwDir * config.throwForce *grabbedObject.mass, ForceMode.Impulse);

        Release();
    }

    void Release()
    {
        if (grabbedObject != null)
        {
            grabbedObject.useGravity = true;
            grabbedObject.linearDamping = 0f;

            grabbedObject.constraints = RigidbodyConstraints.None;
        }

        grabbedObject = null;
        holdingOverhead = false;

        PlayerStats.GrabMode = false;

        //Debug.Log("SOLTOU");
    }

    void CheckHighlight()
    {
        RaycastHit hit;

        if (Physics.Raycast(grabPoint.position,grabPoint.forward,out hit,config.grabDistance,interactionLayer))
        {
            Renderer r = hit.collider.GetComponentInParent<Renderer>();

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