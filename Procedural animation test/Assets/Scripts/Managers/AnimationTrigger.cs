using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationTrigger : MonoBehaviour
{
    public string name;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) anim.SetTrigger(name);
    }
}
