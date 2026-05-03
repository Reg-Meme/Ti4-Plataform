using UnityEngine;

public class Activateable : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Activate()
    {
        animator.SetBool("Active", true);
    }

    public void Deactivate()
    {
        animator.SetBool("Active", false);
    }
}
