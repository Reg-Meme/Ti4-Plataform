using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CutsceneEnd : MonoBehaviour
{
    public CanvasGroup fade;
    public float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
     void Update()
    {
        
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            fade.DOFade(1, 1f).SetUpdate(true).OnComplete(() => SceneManager.LoadScene(2));

        }
        else
        {
            fade.DOFade(0, 14f).SetUpdate(true);
        }
    }
}