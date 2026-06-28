using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public static bool alredyTaked = false;
    void OnTriggerEnter(Collider other)
    {
        if (alredyTaked) return;
       // if (other.CompareTag("Player"))
        //SoundtrackManager.instance.SetMusic(musica,verdade ou mentira para loop);
    }
}
