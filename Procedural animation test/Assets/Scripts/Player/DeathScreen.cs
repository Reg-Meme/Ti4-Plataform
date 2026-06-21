using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
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
    UiControl MenuUi;
    public float currentValue;
    [SerializeField] private InputInfo inputInfo;
    public static DeathScreenAni deathScreen;
    void Start()
    {
        MenuUi = GetComponent<UiControl>();
        input = GetComponent<PlayerInput>();
        audioListener = GetComponent<AudioListener>();
        DeathScreen.SetActive(false);
        audioListener.enabled = false;

        input.enabled = false;
        InputInfo.LevelChange += Killer;
        if(deathScreen == null) deathScreen = this;
    }

  public void ExitToMenu()
    {
        Debug.LogWarning("exit");
        SceneManager.LoadScene("MainMenu"); 
    }

    public void RestartFromTheLastCheckpoint()
    {
         Debug.LogWarning("restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        inputInfo.Initialize();
    }

    public void ShowDeathScreen()
    {
        DeathScreen.SetActive(true);
        DeathScreenBg.DOFade(1, tweenDur).SetUpdate(true);
        DeathScreenInfo.DOFade(1, tweenDur + 1.5f).SetUpdate(true);
        MenuUi.enabled = false;
    }

    public void Killer()
    {
       Debug.LogError("|Estou na builda");
        PlayerStats.IsDead = true;
        Destroy(Player);
        GameManager.Instance.inputInfo.SetUi();
        EventSystem.current.SetSelectedGameObject(CurrentButton);
        audioListener.enabled = true;
        

    }

}
