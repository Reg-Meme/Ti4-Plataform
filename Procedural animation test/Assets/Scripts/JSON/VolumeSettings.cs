using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioMixer mixer;
    private string saveFilePath;
    [System.Serializable]
    public class VolumeData
    {
        public float musicVolume = 1f;
        public float sfxVolume = 1f;
    }

    void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "volumeSettings.json");

        if (File.Exists(saveFilePath))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSfxVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        float clampedVolume = Mathf.Max(volume, 0.0001f); 
        mixer.SetFloat("SounTrack", Mathf.Log10(clampedVolume) * 20);
        SaveVolume();
    }

    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        float clampedVolume = Mathf.Max(volume, 0.0001f);

        mixer.SetFloat("SFX", Mathf.Log10(clampedVolume) * 20);
        SaveVolume();
    }

    private void SaveVolume()
    {

        VolumeData data = new VolumeData();
        data.musicVolume = musicSlider.value;
        data.sfxVolume = sfxSlider.value;
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
    }

    private void LoadVolume()
    {
        string json = File.ReadAllText(saveFilePath);
        VolumeData data = JsonUtility.FromJson<VolumeData>(json);  
        musicSlider.value = data.musicVolume;
        sfxSlider.value = data.sfxVolume;
        SetMusicVolume();
        SetSfxVolume();
    }
}