using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AniManager : MonoBehaviour
{
    public Animator Ani;
    Moviment mov;
    public Rig Damp;
    public Transform BladeRef;
    Use Use;
    
    SlashMechanic slash;
    Vector3 DefaultRefPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ani = Ani.GetComponent<Animator>();
        mov = GetComponent<Moviment>();
        Use = GetComponent<Use>();
        slash = Use.Slash;
        DefaultRefPos = new Vector3(20,0,0);
    }

    // Update is called once per frame
    public void FlipRef()
    {
        if (slash.bladeMode)
        {
            BladeRef.position = -BladeRef.position;
        }
        else
        {
            BladeRef.position = DefaultRefPos;
        }
        
    }
    void Update()
    {
        Ani.SetFloat("x",BladeRef.position.x);
        Ani.SetFloat("y",BladeRef.position.y);
        
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
