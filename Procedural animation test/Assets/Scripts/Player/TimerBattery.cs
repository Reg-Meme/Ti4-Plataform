using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class DeathScreenAni : MonoBehaviour
{
    public PlayerInput Input;
    public float BatteryTim = 390f;
    public float Fill = 1.5f;
    public float EffectActiv = 1.3f;
    [SerializeField] GameObject DeathScreen;
    [SerializeField] GameObject CurrentButton;
    public CanvasGroup DeathScreenBg;
    public CanvasGroup DeathScreenInfo;
    public float tweenDur;
    public float MaxEffectPower = 0.8f;
    public float maxVignetteSize = 0.8f;
    Material LowBatMat;
    float timer;
    
    public float currentValue;

    void Start()
    {
        timer = BatteryTim;
        currentValue = Fill;
        DeathScreen.SetActive(false);
    }
    
     
    
    void Update()
    {
        if (PlayerStats.IsDead == true)
        {
            EventSystem.current.SetSelectedGameObject(CurrentButton);
            DeathScreen.SetActive(true);
            Input.SwitchCurrentActionMap("UI");
            DeathScreenBg.DOFade(1,tweenDur).SetUpdate(true);
            DeathScreenInfo.DOFade(1,tweenDur+1.5f).SetUpdate(true);
        }
    }

    public void Killer()
    {
        PlayerStats.IsDead = true;
    }
}
