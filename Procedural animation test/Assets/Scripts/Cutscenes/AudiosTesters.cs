using UnityEngine;

public class AudiosTesters : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        audioSource1.mute=false;
        audioSource2.mute=false;
    }
}
