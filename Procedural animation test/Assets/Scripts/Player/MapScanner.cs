using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.InputSystem;
using DG.Tweening;
public class MapScanner : MonoBehaviour
{
    public VisualEffect VFX;
    public GameObject ScannerObj;
    PlayerInput Input;
    [SerializeField] InputActionReference Map;
    [SerializeField] InputInfo inputInfo;
    
    [SerializeField] CanvasGroup BatteryFade;
    [SerializeField] RectTransform Battery;
    public float BatteryStartAnchor;
    public float BatteryFinalAnchor;
    public float TweenDur;
    public float FadeTime;
    public float FadeTimeTwo;
    public float lerpSpd; 
    public float ScanTim;
    private bool Scanner;
    private float timer;
    private bool NoloopVFX;
    private float currentClippingValue = 3f;

    void Start()
    {
        Input = GetComponent<PlayerInput>();
        ScannerObj.SetActive(false);
        Shader.SetGlobalFloat("_MapBGClipping", 3f);
        InputInfo.OnMapEvent += MapActive;
    }
    public void MapActive()
    {
         Scanner = true;
    }

    void Update()
    {
        float targetValue;

        if (Scanner)
        {

            if (!NoloopVFX)
            {
                VFX.SendEvent("OnPlay");
                Battery.DOAnchorPosX(BatteryFinalAnchor, TweenDur).SetEase(Ease.InSine);
            }
            
            targetValue = 0f;
            ScannerObj.SetActive(true);
            timer = ScanTim;
            
            
            BatteryFade.DOFade(1, FadeTime);
            Scanner = false;
        }
        else
        {
            
            timer -= Time.deltaTime;
            targetValue = (timer > 0) ? 0f : 3f;
            
            if (timer <= 0)
            {
                ScannerObj.SetActive(false);
                Battery.DOAnchorPosX(BatteryStartAnchor, TweenDur).SetEase(Ease.OutSine);
                BatteryFade.DOFade(0, FadeTimeTwo).SetEase(Ease.OutSine);
            }
        }
        currentClippingValue = Mathf.Lerp(currentClippingValue, targetValue, Time.deltaTime * lerpSpd);
        Shader.SetGlobalFloat("_MapBGClipping", currentClippingValue);

        NoloopVFX = Scanner;
    }
}
