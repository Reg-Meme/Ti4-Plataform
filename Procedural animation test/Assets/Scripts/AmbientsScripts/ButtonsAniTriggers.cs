using UnityEngine;

public class ButtonsAniTriggers : MonoBehaviour
{
    public string TriggerAni;
    public Animator ani;
    Animator ani2;
    public bool ObjectToggle;
    public GameObject[] ObjectsToggle;

     void Start()
    {
       
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        ani.SetTrigger(TriggerAni);
        ani2.SetTrigger("Trigger");
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
