using UnityEngine;
using UnityEngine.VFX;

public class DeathByColider : MonoBehaviour
{
    DeathScreenAni Death;
    public GameObject Player;
    public GameObject SplashVfx;
    public VisualEffect ploftVfx;
    public float Offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Death =  FindAnyObjectByType<DeathScreenAni>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SplashVfx.transform.position = new Vector3(Player.transform.position.x,Player.transform.position.y+Offset,Player.transform.position.z);
            ploftVfx.SendEvent("OnPlay");
            Death.Killer();
            Death.ShowDeathScreen();
        }   
    }
}
