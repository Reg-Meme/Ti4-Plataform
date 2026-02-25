using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager Shaker;
    public CinemachineBasicMultiChannelPerlin CamShake;
    
    private Coroutine shakeCoroutine;

    void Awake()
    {
        if (Shaker == null) Shaker = this;
        else Destroy(gameObject);
    }

    public void ShakePulse(float CamShakeAmp, float CamShakeFreq, float Dur)
    {
        if (OnOffOptions.Instance.CameraShake)
        {
            if (CamShake != null)
            {
                if (shakeCoroutine != null) StopCoroutine(shakeCoroutine);    
                shakeCoroutine = StartCoroutine(ShakeNature(CamShakeAmp, CamShakeFreq, Dur));
            }
        }
    }

    private IEnumerator ShakeNature(float startAmp, float startFreq, float Dur)
    {
        float elapsedTime = 0;
        CamShake.AmplitudeGain = startAmp;
        CamShake.FrequencyGain = startFreq;

        while (elapsedTime < Dur)
        {
            elapsedTime += Time.deltaTime;
            float lerpPercent = elapsedTime / Dur;

            CamShake.AmplitudeGain = Mathf.Lerp(startAmp, 0f, lerpPercent);
            CamShake.FrequencyGain = Mathf.Lerp(startFreq, 0f, lerpPercent);
            yield return null;
        }
        CamShake.AmplitudeGain = 0f;
        CamShake.FrequencyGain = 0f;
        shakeCoroutine = null;
    }
}
