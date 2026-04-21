using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.VFX;
using Unity.Cinemachine;
using JetBrains.Annotations;
public class AniManager : MonoBehaviour
{
    public Animator Ani;
    Moviment mov;
    public Rig Damp;
    [Header("BladeModeVFX")]
    public Transform BladeRef;
    public Transform VFXRef;
    public CameraShakeManager Camshake;
    public VisualEffect CutVFX;
    public float Freq;
    public float Amp;
    public float Dur;
    SlashMechanic slash;
    Use Use;
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
    public void BladeAni()
    {
        if (slash.bladeMode)
        {
            BladeRef.position = -BladeRef.position;
        }
        else
        {
            BladeRef.position = DefaultRefPos;
            CutVFX.SendEvent("OnExit");
        }
        CutVFX.SendEvent("OnPlay");
        CutVFX.transform.rotation = VFXRef.transform.rotation;
        CutVFX.SetVector3("Direction",new Vector3(VFXRef.position.x,VFXRef.position.y,0));
        Camshake.ShakePulse(Freq,Amp,Dur);
        
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
           CutVFX.SendEvent("OnExit");
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
