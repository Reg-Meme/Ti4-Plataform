using UnityEngine;

public class BasicIkFootSolver : MonoBehaviour
{
    public Transform bodyTarget;
    public LayerMask groundLayer;
    public float RayDis = 1.5f;
    public float Spd = 10f;
    Vector3 TargetOffset;
    Quaternion TargetRottOffset;
    Vector3 CurrentOffset; 
    float Lerp;
    float NewPos;
    float OldPos;

     void Start()
    {
            TargetOffset = bodyTarget.InverseTransformPoint(transform.position);
            TargetRottOffset = Quaternion.Inverse(bodyTarget.rotation) * transform.rotation;
    }

    void Update()
    {
        Vector3 worldPos = bodyTarget.TransformPoint(TargetOffset);
        transform.rotation = bodyTarget.rotation * TargetRottOffset;
        FootNature(worldPos);
    }

    void FootNature(Vector3 CurrentPos)
    {
        Ray ray = new Ray(CurrentPos + Vector3.up, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, RayDis, groundLayer))
        {
            float groundY = hit.point.y;
            float differenceY = groundY - CurrentPos.y;
            if (Mathf.Abs(NewPos - differenceY) > 0.05f)
            {
                Lerp = 0;
                OldPos = CurrentOffset.y;
                NewPos = differenceY;
            }
        }

        if (Lerp < 1)
        {
            Lerp += Time.deltaTime * Spd;
            CurrentOffset.y = Mathf.Lerp(OldPos, NewPos, Lerp);
        }

        transform.position = CurrentPos + Vector3.up * CurrentOffset.y;
    }
}
