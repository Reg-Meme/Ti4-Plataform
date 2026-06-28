using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioClip clip;
    public AudioClip proximoClip;
    public bool loop;
    public float transitionTime = 2f;
    public bool haveTransition;
    public bool taked = false;
    void Start ()
    {
           //StartCoroutine(SoundtrackManager.instance.MusicEnd(clip, (c) => SoundtrackManager.instance.SetMusic(proximoClip)));
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(!taked)
        {
            
        if (other.CompareTag("Player"))
        {
            if(!haveTransition)
            {
                Debug.Log("n transi");
            if (clip != null) SoundtrackManager.instance.CrossFade(clip,transitionTime,loop);
            }
            else
            {
            SoundtrackManager.instance.CrossFade(clip,transitionTime,false);
            StartCoroutine(SoundtrackManager.instance.MusicEnd(clip, (c,t,b) => SoundtrackManager.instance.CrossFade(proximoClip,transitionTime,loop)));
            } 
           
           
        }
        taked = true;
        }
    }
}
