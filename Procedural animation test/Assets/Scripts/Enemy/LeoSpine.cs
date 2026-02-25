using Unity.VisualScripting;
using UnityEngine;

public class LeoSpine : MonoBehaviour
{
    public Transform target;
    public Vector3 offset; 
    public float Smooth;
    public float Spd;
    [Range(-1f, 1f)]
    public float thresholdL = 0.5f;
    [Range(-1f, 1f)]
    public float thresholdR = -0.5f;
    Vector3 Vel = Vector3.zero;
    void Update()
    {
        Vector3 targetPos = target.TransformPoint(offset);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref Vel, Smooth);
        float dot = Vector3.Dot(transform.forward, target.forward);

        if (dot < thresholdL || dot > thresholdR )
        {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Spd * Time.deltaTime);
        }
    }

}