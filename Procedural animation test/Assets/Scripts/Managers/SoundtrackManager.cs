using System;
using System.Collections;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Playables;
public class SoundtrackManager : MonoBehaviour
{
    public static SoundtrackManager instance;
    public double time;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public bool usingSource1 = true;
    public PlayableDirector diretor;
    public AudioClip clip;
    public float transitionTime = 2f;
    void Awake()
    {
        if (instance == null) instance = this;
        
    }
    public void Start()
    {
        if (PlayerStats.clip != null)
            audioSource2.clip = PlayerStats.clip;
        if (PlayerStats.musicTime != 0)
            audioSource2.time = PlayerStats.musicTime;
       StartCoroutine(MusicEnd(audioSource1.clip, (c,f,t) => CrossFade(clip,transitionTime, true)));
    }
    public void PlayMusic(AudioClip clip)
    {
        audioSource2.clip = clip;
        audioSource2.Play();
    }
    public void SetMusic(AudioClip clip, bool loop)
    {
        audioSource2.loop = loop;
        audioSource2.clip = clip;
        audioSource2.Play();
      
    }
    public void SetLoop(bool loop)
    {
        audioSource2.loop = loop;

    }
    public void CheckLoop()
    {
        if (audioSource2.loop)
            diretor.time = time;

    }
    public void CrossFade(AudioClip newClip, float transitionTime = 2f, bool loop = true)
    {
        AudioSource audioSource = usingSource1 ? audioSource1 : audioSource2;
        if(audioSource == newClip) return;

        StartCoroutine(Fade(newClip, transitionTime, loop));
    }
    private IEnumerator Fade(AudioClip clip, float transitionTime, bool loop)
    {
        AudioSource nowPlaying = usingSource1 ? audioSource1 : audioSource2;
        AudioSource nextPlaying = usingSource1 ? audioSource2 : audioSource1;

        nextPlaying.clip = clip;
        nextPlaying.volume = 0;
        nextPlaying.Play();
        
        float perfomedTime = 0;
        float defaultVolume = nowPlaying.volume;

        while (perfomedTime < transitionTime)
        {
             perfomedTime += Time.deltaTime;

             float percent = perfomedTime / transitionTime;

             nowPlaying.volume = Mathf.Lerp(defaultVolume, 0f, percent);
             nextPlaying.volume = Mathf.Lerp(0f,1f, percent);

             yield return null;
        }
        nextPlaying.volume = 1f;
        nextPlaying.loop = loop;
        nowPlaying.volume = 0;
        nowPlaying.Stop();

        usingSource1 = !usingSource1;

    }

   
    public IEnumerator MusicEnd(AudioClip clip, Action<AudioClip,bool,float> endFunction)
    {
       
        PlayMusic(clip);
        yield return new WaitForSeconds(clip.length-transitionTime);
        if (!audioSource2.loop)
        {
            
              endFunction?.Invoke(clip,true,0);
        }
        else StartCoroutine(MusicEnd(clip, endFunction));
    }





    // Update is called once per frame

}
