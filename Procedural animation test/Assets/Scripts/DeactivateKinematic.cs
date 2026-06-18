using UnityEngine;

public class DeactivateKinematic : MonoBehaviour
{
    [Header("Config")]
    public bool oneShot = false;

    [Tooltip("Rigidbodies que terão a física ativada")]
    public Rigidbody[] rbs;

    [Tooltip("Tag do objeto que ativa a trigger")]
    public string collisionTag;

    [Header("Collider Opcional")]
    public bool alterarCollider = false;
    public bool habilitarCollider = false;
    public BoxCollider[] colliders;

    private bool alreadyEntered = false;

    void Awake()
    {
        if (rbs == null || rbs.Length == 0) rbs = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rbs)
        {
            if (rb != null) rb.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (alreadyEntered) return;

        if (!string.IsNullOrEmpty(collisionTag) &&!collision.CompareTag(collisionTag))
            return;

        foreach (Rigidbody rb in rbs)
        {
            if (rb != null) rb.isKinematic = false;
        }

        if (alterarCollider)
        {
            foreach (BoxCollider col in colliders)
            {
                if (col != null) col.enabled = habilitarCollider;
            }
        }

        if (oneShot) alreadyEntered = true;
    }
}