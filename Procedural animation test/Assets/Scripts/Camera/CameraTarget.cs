using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        Vector3 pos = player.position;
        pos.y += 1.5f;

        transform.position = pos;

        Vector3 forward = player.forward;
        forward.y = 0;

        if (forward.sqrMagnitude > 0.01f)
        {
            transform.rotation =
                Quaternion.LookRotation(forward);
        }
    }
}