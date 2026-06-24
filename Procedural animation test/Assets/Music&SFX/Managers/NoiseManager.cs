using UnityEngine;

public class InterAreasBackNoiseManager : MonoBehaviour
{
    public AudioSource Ocean;
    public AudioSource Wind;
    
    [Range(0f, 1f)] public float InVol = 1f;
    [Range(0f, 1f)] public float OutVol = 0.2f;
    
    [Tooltip("Tempo em segundos para a transição do volume terminar.")]
    public float transitionSpeed = 2f;

    private float targetVolume;

    private void Start()
    {
        // Define o volume inicial com base no valor de fora (ou de dentro, mude se preferir)
        targetVolume = InVol;
        Ocean.volume = InVol;
        Wind.volume = InVol;
    }

    private void Update()
    {
        // Faz a transição suave frame a frame até atingir o volume alvo
        float currentVolume = Mathf.MoveTowards(Ocean.volume, targetVolume, Time.deltaTime / transitionSpeed);
        
        Ocean.volume = currentVolume;
        Wind.volume = currentVolume;
    }

    public void OnTriggerEnter(Collider other)
    {
        // Verifica se quem entrou foi o Player (boa prática para evitar bugs com outros objetos)
        if (other.CompareTag("Player"))
        {
            targetVolume = InVol;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetVolume = OutVol;
        }
    }
}