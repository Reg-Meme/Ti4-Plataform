using UnityEngine;
using UnityEngine.VFX;
public class SecurityDoorTriggerEvents : MonoBehaviour
{
    public VisualEffect vfx1;
    public VisualEffect vfx2;
    public VisualEffect vfx3;
    public VisualEffect vfx4;
    public VisualEffect vfx5;
    public VisualEffect vfx6;
    public VisualEffect vfx7;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySfx()
    {
        SfXManager.soundManager.PlaySound(SfXManager.SoundType.SecurityDoor);
    }
    public void OnPlay()
    {
        vfx1.SendEvent("OnPlay");
        vfx2.SendEvent("OnPlay"); 
        vfx3.SendEvent("OnPlay"); 
        vfx4.SendEvent("OnPlay");
        
    }
    public void OnPlay2()
    {
        vfx5.SendEvent("OnPlay");
        
    
    }
    public void OnPlay3()
    {
        vfx6.SendEvent("OnPlay");
        vfx7.SendEvent("OnPlay");
    }
    public void OnExit()
    {
        vfx1.SendEvent("OnStop"); 
        vfx2.SendEvent("OnStop"); 
        vfx3.SendEvent("OnStop"); 
        vfx4.SendEvent("OnStop");
    }

    public void OnExit2()
    {
        vfx5.SendEvent("OnStop");

    }
    public void OnExit3()
    {
        vfx6.SendEvent("OnStop");
        vfx7.SendEvent("OnStop");
    }

}

