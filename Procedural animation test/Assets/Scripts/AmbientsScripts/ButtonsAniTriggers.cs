using UnityEngine;

public class ButtonsAniTriggers : MonoBehaviour
{
    public string TriggerAni;
    public Animator ani;
    Animator ani2;
    public bool ObjectToggle;
    public GameObject[] ObjectsToggle;
    public bool IsOpen;

     void Start()
    {
        ani2= GetComponent<Animator>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        ani2.SetTrigger("Trigger");
        ani.SetTrigger(TriggerAni);
        
        if (ObjectToggle)
        {
            foreach(GameObject obj in ObjectsToggle)
            {
               if (obj != null)
            {
                obj.SetActive(false);
            }
            }
        }
       // IsOpen = true;
    }
}
