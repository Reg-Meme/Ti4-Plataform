using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AniManager : MonoBehaviour
{
    public Animator Ani;
    Moviment mov;
    public Rig Damp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ani = Ani.GetComponent<Animator>();
        mov = GetComponent<Moviment>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(mov.BottleMode == true)
        {
           Ani.SetBool("Bottle", true);
           Damp.weight = 0;
           
        }
        else
        {
           Damp.weight = 1;
           Ani.SetBool("Bottle", false); 
        }
    }
}
