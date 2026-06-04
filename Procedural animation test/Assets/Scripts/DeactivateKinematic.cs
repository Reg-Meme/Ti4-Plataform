using UnityEngine;

public class DeactivateKinematic : MonoBehaviour
{
    [Header("Config")]
    public bool oneShot = false;

    [Tooltip("Rigidbody que terá a física ativada")]
    public Rigidbody cano;

    [Tooltip("Tag do objeto que ativa a trigger")]
    public string collisionTag = "Player";

    private bool alreadyEntered = false;

    void Awake()
    {
        if (cano == null) cano = GetComponent<Rigidbody>();

        if (cano != null) cano.isKinematic = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (alreadyEntered) return;

        if (!string.IsNullOrEmpty(collisionTag) && !collision.CompareTag(collisionTag)) return;

        cano.isKinematic = false;

        if (oneShot) alreadyEntered = true;
    }
}