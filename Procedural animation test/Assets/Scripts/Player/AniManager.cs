using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AniManager : MonoBehaviour
{
    public Animator Ani;
    Moviment mov;
    public Rig Damp;
    Use Use;
    SlashMechanic slash;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ani = Ani.GetComponent<Animator>();
        mov = GetComponent<Moviment>();
        Use = GetComponent<Use>();
        slash = Use.Slash;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(mov.BottleMode == true)
        {
            Damp.weight = 0;
           Ani.SetBool("Bottle", true);
        }
        else
        {
           Damp.weight = 1;
           Ani.SetBool("Bottle", false); 
        }

        if(slash.bladeMode == true)
        {
            Ani.SetBool("Blade", true);
            Damp.weight = 0;
        }
        else
        {
           Ani.SetBool("Blade", false); 
        }

    }
    public void NoDamp()
    {
        Damp.weight = 0;
    }
    public void YeaDamp()
    {
        Damp.weight = 1;
    }
}
