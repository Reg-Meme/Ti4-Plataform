using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BatteryTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float BatteryTim = 390f;
    public float Fill = 1.5f;
    public float EffectActiv = 1.3f;
    [SerializeField] FullScreenPassRendererFeature LowBatteryEffect;
    public float MaxEffectPower = 0.8f;
    public float maxVignetteSize = 0.8f;
    Material LowBatMat;
    float timer;
    
    public float currentValue;

    void Start()
    {
        timer = BatteryTim;
        currentValue = Fill;
        LowBatMat = LowBatteryEffect.passMaterial;
        LowBatteryEffect.SetActive(false);
    }
     
    
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            float ratio = timer / BatteryTim;
            currentValue = Mathf.Clamp(Fill * ratio, 0, Fill);
            Shader.SetGlobalFloat("_Fill", currentValue);

            if (currentValue <= EffectActiv)
            {
                float t = Mathf.InverseLerp(EffectActiv, 0, currentValue);
                LowBatteryEffect.SetActive(true);
                Shader.SetGlobalFloat("_EffectPower", t * MaxEffectPower);
                Shader.SetGlobalFloat("_VinhetteSize", t * maxVignetteSize);
            }
        }
        else
        {
            timer = 0;
            currentValue = 0;
            //Shader.SetGlobalFloat("_EffectPower",0);
           // Shader.SetGlobalFloat("_VinhetteSize",2);
        }
    }
}
