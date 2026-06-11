using UnityEngine;

public class StaticCameraLookAt : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation,rotationSpeed * Time.deltaTime);
    }
}