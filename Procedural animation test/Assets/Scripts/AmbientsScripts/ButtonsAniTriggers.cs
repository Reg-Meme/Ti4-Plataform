using UnityEngine;

public class ButtonsAniTriggers : MonoBehaviour
{
    public string TriggerAni;
    public Animator ani;
    Animator ani2;
     void Start()
    {
        ani2 = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        ani.SetTrigger(TriggerAni);
        ani2.SetTrigger("Trigger");
        
    }
}
