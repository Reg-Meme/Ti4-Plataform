using UnityEngine;
using UnityEngine.VFX;

public class DeathByColider : MonoBehaviour
{
    DeathScreenAni Death;
    public GameObject Player;
    public GameObject SplashVfx;
    public VisualEffect ploftVfx;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Death =  FindAnyObjectByType<DeathScreenAni>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SplashVfx.transform.position = new Vector3(Player.transform.position.x,Player.transform.position.y+0.02f,Player.transform.position.z);
            ploftVfx.SendEvent("OnPlay");
            Death.Killer();
            
            Death.ShowDeathScreen();
        } 
        
        
    }
}
