using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SfXManager
{

    public enum SoundType

    {

        SecurityDoor,
        Crane,
        

    }

    [ExecuteInEditMode]
    public class soundManager : MonoBehaviour
    {

        [SerializeField] private SoundList[] soundList;
        public static soundManager instance;
       
        public AudioSource musicSource;

      

        public void Start()
        {
           
        }

        public void Awake()
        {
            instance = this;
        }

        public static void PlaySound(SoundType sound, float volume = 1f)
        {
            AudioClip[] clips = instance.soundList[(int)sound].Sounds;
            
            AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
            instance.musicSource.PlayOneShot(randomClip, volume);
          
            
           
            

        }
        public static void StopSound(SoundType sound, float volume = 1f)
        {
            AudioClip[] clips = instance.soundList[(int)sound].Sounds;

            AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

            instance.musicSource.Stop();

        }
#if UNITY_EDITOR
        private void OnEnable()
        {
            string[] names = Enum.GetNames(typeof(SoundType));
            Array.Resize(ref soundList, names.Length);
            for (int i = 0; i < soundList.Length; i++)
            {
                soundList[i].name = names[i];
            }
        }
    #endif
    
    }
    
    
    [Serializable]
    public struct SoundList
    {
        public AudioClip[] Sounds {get => sounds;}
        [HideInInspector] public string name;
        [SerializeField] public AudioClip[] sounds;
    }
}
