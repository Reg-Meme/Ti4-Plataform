using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class DeathScreenAni : MonoBehaviour
{
    //public InputInfo Input;
    [SerializeField] GameObject DeathScreen;
    [SerializeField] GameObject CurrentButton;
    public CanvasGroup DeathScreenBg;
    public CanvasGroup DeathScreenInfo;
    public float tweenDur;
    public float MaxEffectPower = 0.8f;
    public float maxVignetteSize = 0.8f;
    public GameObject Player;
    AudioListener audioListener;
    PlayerInput input;
    
    public float currentValue;

    void Start()
    {
        input= GetComponent<PlayerInput>();
        audioListener= GetComponent<AudioListener>();
        DeathScreen.SetActive(false);
        audioListener.enabled=false;
        Player = GameObject.FindGameObjectWithTag("Player");
        input.enabled= false;
    }
    
     
    
    void Update()
    {
        if (PlayerStats.IsDead == true)
        {
            EventSystem.current.SetSelectedGameObject(CurrentButton);
            input.SwitchCurrentActionMap("Ui");
        }
    }

    public void ShowDeathScreen()
    {
            DeathScreen.SetActive(true);
            DeathScreenBg.DOFade(1,tweenDur).SetUpdate(true);
            DeathScreenInfo.DOFade(1,tweenDur+1.5f).SetUpdate(true);
    }

    public void Killer()
    {
        PlayerStats.IsDead = true;
        Destroy(Player);
        audioListener.enabled= true;
        input.enabled= true;
    }

}
