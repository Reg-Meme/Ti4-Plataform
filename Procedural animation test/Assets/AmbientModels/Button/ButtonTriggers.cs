using UnityEngine;

public class ButtonTriggers : MonoBehaviour
{
    [SerializeField] bool _isHeavy;
    [SerializeField] Animator animator;
    [SerializeField] private Activateable[] activates;
    private int colCount;
    
    void Start()
    {
        colCount = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_isHeavy)
        {
            if(collision.collider.attachedRigidbody.mass < 10) return;
        }

        colCount++;
        if(colCount == 1)
        {
            animator.SetBool("Pressed", true);
            foreach(Activateable act in activates)
            {
                act.Activate();
            }
        }

    }

    void OnCollisionExit(Collision collision)
    {
        if (_isHeavy)
        {
            if(collision.collider.attachedRigidbody.mass < 10) return;
        }

        colCount--;
        if(colCount != 0) return;

        animator.SetBool("Pressed", false);
        foreach(Activateable act in activates)
        {
            act.Deactivate();
        }
    }
}
