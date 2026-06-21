using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class NewGame : MonoBehaviour
{
    public GameObject panelConfirm;
    public CanvasGroup PanelGroup;
    public GameObject continueBtn;
    public GameObject newGameBtn;
    public GameObject NoBtn;
    public EventSystem eventSystem;

    public float tweendur;
    void Start()
    {
        if (PlayerStats.LoadStats() != null)
        {
            eventSystem.firstSelectedGameObject = continueBtn;
            continueBtn.SetActive(true);
        }
        else
        {
            eventSystem.firstSelectedGameObject = newGameBtn;
            continueBtn.SetActive(false);
        }
    }
    public void StartLv1()
    {
        //StartCoroutine(SceneSwitch());

        //     SceneManager.LoadSceneAsync("Gameplay",LoadSceneMode.Additive);

        SceneManager.LoadScene("Ctscene1");
        PlayerStats.lastScene = "1Level";
        PlayerStats.ClearStats();


        //        SceneManager.UnloadSceneAsync("MainMenu");

    }
    public void CheckStats()
    {
        if (PlayerStats.LoadStats() == null) StartLv1();
        else SafeNewGame();
    }

    public void SafeNewGame()
    {
        PanelGroup.DOFade(1, tweendur);
        panelConfirm.SetActive(true);
        EventSystem.current.SetSelectedGameObject(NoBtn);
    }
    public async void No()
    {
        await  PanelGroup.DOFade(0, 1f).SetUpdate(true).AsyncWaitForCompletion();
        panelConfirm.SetActive(true);
        EventSystem.current.SetSelectedGameObject(newGameBtn);
    }
    
    public void Cotinue()
    {
        SceneManager.LoadScene(PlayerStats.lastScene);
    }
    IEnumerator SceneSwitch()
    {
        SceneManager.LoadSceneAsync("Gameplay", LoadSceneMode.Additive);
        AsyncOperation load = SceneManager.LoadSceneAsync("1Level", LoadSceneMode.Additive);
        GameManager.Instance.name = "1Level";
        yield return load;
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
