using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RainZone : BatteryTimer
{
    [SerializeField] FullScreenPassRendererFeature rainEffect;
    
    [SerializeField] float Dur = 2.0f;
    [SerializeField] float maxSpeed = 4.43f;
    [SerializeField] public Vector2 maxScale = new Vector2(17.68f, 1.47f);
    [SerializeField] float maxContrast = 0.06f;

    Material RainMat;
    Coroutine transitionCoroutine;

    public void Start()
    {
        RainMat = rainEffect.passMaterial;
        
        rainEffect.SetActive(false);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Dur = 2.0f;
        if (other.CompareTag("Player")) StartTransition(true);
        
    }

    private void OnTriggerExit(Collider other)
    {
        Dur = 0.5f;
        if (other.CompareTag("Player")) StartTransition(false);
        
    }
    
    private void StartTransition(bool entering)
    {
        if (RainMat == null) return;
        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(TransitionRain(entering));
    }

    private IEnumerator TransitionRain(bool entering)
    {
        if (entering) rainEffect.SetActive(true);

        float elapsed = 0;
        
        float startSpd = RainMat.GetFloat("_Spd");
        Vector2 startScale = RainMat.GetVector("_Scale");
        float startContrast = RainMat.GetFloat("_NoiseContrast");

        float targetSpd = entering ? maxSpeed : 0f;
        Vector2 targetScale = entering ? maxScale : Vector2.zero;
        float targetContrast = entering ? maxContrast : 0f;

        while (elapsed < Dur)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / Dur;
            RainMat.SetFloat("_Spd", Mathf.Lerp(startSpd, targetSpd, t));
            RainMat.SetVector("_Scale", Vector2.Lerp(startScale, targetScale, t));
            RainMat.SetFloat("_NoiseContrast", Mathf.Lerp(startContrast, targetContrast, t));

            yield return null;
        }
        RainMat.SetFloat("_Spd", targetSpd);
        RainMat.SetVector("_Scale", targetScale);
        RainMat.SetFloat("_NoiseContrast", targetContrast);

        if (!entering) rainEffect.SetActive(false);
    }
}