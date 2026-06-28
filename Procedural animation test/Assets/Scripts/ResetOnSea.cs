using UnityEngine;

public class ResetOnSea : MonoBehaviour
{
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    [SerializeField] private Transform _resetPoint;

    void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;

        if (_resetPoint == null)
        {
            Debug.LogWarning("[ResetOnSea] Nenhum GameObject com a tag 'RO' foi encontrado na cena!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sea")) ResetObject();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sea")) ResetObject();
    }

    private void ResetObject()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (_resetPoint != null)
        {
            transform.position = _resetPoint.position;
            transform.rotation = _resetPoint.rotation;
        }
        else
        {
            transform.position = _startPosition;
            transform.rotation = _startRotation;
        }
    }
}