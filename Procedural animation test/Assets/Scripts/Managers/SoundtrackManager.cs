using UnityEngine;

public class SoundtrackManager : MonoBehaviour
{
    public static SoundtrackManager instance;
    public static float time;
    public AudioSource audioSource;
    public void Start()
    {
        if (instance == null) instance = this;
        if (PlayerStats.clip != null)
            audioSource.clip = PlayerStats.clip;
        if (PlayerStats.musicTime != 0)
            audioSource.time =PlayerStats.musicTime ;
        audioSource.Play();
    }
    public void PlayMusic(AudioSource audioSource)
    {
        if (audioSource == null) return;
        audioSource.Play();
    }
    public void SetMusic(AudioClip clip, bool loop)
    {
        audioSource.clip=clip;
        audioSource.loop = loop;
    }


    // Update is called once per frame

}
