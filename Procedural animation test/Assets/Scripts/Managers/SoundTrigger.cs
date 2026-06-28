using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioClip clip;
    public AudioClip proximoClip;
    public bool loop;
    public float transitionTime = 2f;
    public bool haveTransition;
    void Start ()
    {
           //StartCoroutine(SoundtrackManager.instance.MusicEnd(clip, (c) => SoundtrackManager.instance.SetMusic(proximoClip)));
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if(!haveTransition)
            if (clip != null) SoundtrackManager.instance.CrossFade(clip,transitionTime,loop);
            else StartCoroutine(SoundtrackManager.instance.MusicEnd(clip, (c,t,b) => SoundtrackManager.instance.CrossFade(proximoClip,transitionTime,loop)));
            Destroy(gameObject);
        }
    }
}
