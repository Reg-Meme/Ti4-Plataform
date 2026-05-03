using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    [SerializeField] private Transform toFollow;

    public void SetFollow(Transform toFollow)
    {
        this.toFollow = toFollow;
    }

    void FixedUpdate()
    {
        transform.position = toFollow.position;
    }
}
