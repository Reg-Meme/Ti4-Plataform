using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.VFX;
using Unity.Cinemachine;
using JetBrains.Annotations;
using Unity.Mathematics;
public class AniManager : MonoBehaviour
{
    public Animator Ani;
    Moviment mov;
    public Rig Damp;
    public Rig Legs;
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
    public GameObject Knife;
    public GameObject Magnet;
    public AudioSource RollFrontSFX;
    public AudioSource RollBackSFX;
    public Rigidbody Body;
    bool isSided;
    public float minPitch ;
    public float maxPitch ;
    public float minSpd ;
    public float maxSpd ;

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
        if (PlayerStats.bladeMode)
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
    public void Update()
    {
        
        Ani.SetFloat("x",BladeRef.position.x);
        Ani.SetFloat("y",BladeRef.position.y);
        float legsTarget = !PlayerStats.isJumpig? 1f : 0f;
            float jumpLayerTarget = !PlayerStats.isJumpig ? 0f : 1f;
        float currentLayerWeight = Ani.GetLayerWeight(1);
            float nextLayerWeight = Mathf.Lerp(currentLayerWeight, jumpLayerTarget, Time.deltaTime * 6);
        if(PlayerStats.bottleMode == true)
        {
            Damp.weight = 0;
            Ani.SetBool("Bottle", true);
        
            RollFrontSFX.mute = false;
            RollBackSFX.mute = false;

            float curSpd = Body.angularVelocity.magnitude;

            
            if (curSpd >= minSpd && mov.isGrounded() && PlayerStats.isJumpig == false)
            {
                RollFrontSFX.volume = Mathf.Lerp(RollFrontSFX.volume, 1f, Time.deltaTime * 2f);
                RollBackSFX.volume = Mathf.Lerp(RollBackSFX.volume, 0.2f, Time.deltaTime * 2f);

                float targetPitch = Mathf.Lerp(minPitch, maxPitch, curSpd / maxSpd);
                RollFrontSFX.pitch = Mathf.Lerp(RollFrontSFX.pitch, targetPitch, Time.deltaTime * 100f);
            }
            else
            {
                RollFrontSFX.volume = Mathf.Lerp(RollFrontSFX.volume, 0f, Time.deltaTime * 2f);
                RollBackSFX.volume = Mathf.Lerp(RollBackSFX.volume, 0f, Time.deltaTime * 2f);
                RollFrontSFX.pitch = Mathf.Lerp(RollFrontSFX.pitch, 0f, Time.deltaTime * 10f);  
            }
        }
        else
        {
            RollFrontSFX.pitch = 0;
            RollFrontSFX.volume = Mathf.Lerp(RollFrontSFX.volume, 0f, Time.deltaTime * 100f);
            RollBackSFX.volume = Mathf.Lerp(RollBackSFX.volume, 0f, Time.deltaTime * 100f);
           
            if(Moviment.moviment.move[1] is Roll r)
                isSided = r.IsSided;
                
            Damp.weight = 1;
            Ani.SetBool("Bottle", false);
            
            Legs.weight = Mathf.Lerp(Legs.weight, legsTarget, Time.deltaTime * 8);
            
            Ani.SetLayerWeight(1, nextLayerWeight);
            Ani.SetBool("Jump", PlayerStats.isJumpig);
        }

        if(PlayerStats.bladeMode == true)
        {
            Ani.SetBool("Blade", true);
            Damp.weight = 0;
        }
        else
        {
           Ani.SetBool("Blade", false);
           CutVFX.SendEvent("OnExit");
        }

        if(PlayerStats.bottleMode|| PlayerStats.isJumpig)
        {
            
            Legs.weight = Mathf.Lerp(Legs.weight, legsTarget, Time.deltaTime * 8);
            Ani.SetLayerWeight(1, nextLayerWeight);
            Ani.SetBool("Jump", PlayerStats.isJumpig);  
        }
        if (PlayerStats.GrabMode)
        {
            Ani.SetBool("Grabbing", true);
            Damp.weight = 0;
        }
        else if(!PlayerStats.bottleMode)
        {
            Ani.SetBool("Grabbing", false);
            Damp.weight = 1;
        }
        if (PlayerStats.cutUnlock==false)
        {
            Knife.SetActive(false);
        }
        if (PlayerStats.grabUnlock==false)
        {
            Magnet.SetActive(false);
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
    public void CloseSfx()
    {
        HermitSfXManager.soundManager.PlaySound(HermitSfXManager.SoundType.SwitchToBottle);
    }
    public void Trade1Ani()
    {
        Ani.SetTrigger("Trade1");
    }
    public void Trade2Ani()
    {
        Ani.SetTrigger("Trade2");
    }
    public void KnifeActv()
    {
            Knife.SetActive(true);
    }
    public void MagnetActv()
    {
            Magnet.SetActive(true);
    }
    public void KnifeDesat()
    {
            Knife.SetActive(false);
    }
    public void MagnetDesat()
    {
            Magnet.SetActive(false);
    }
}
