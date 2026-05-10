using UnityEngine;

public class Activateable : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool reversed;

    public void Activate()
    {
        animator.SetBool("Active", !reversed);
    }

    public void Deactivate()
    {
        animator.SetBool("Active", reversed);
    }
}
