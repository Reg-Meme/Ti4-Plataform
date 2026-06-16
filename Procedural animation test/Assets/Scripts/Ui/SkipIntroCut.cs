using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CutsceneButtons : MonoBehaviour
{
    public String Name;
    public CanvasGroup fade;
    public CanvasGroup buttonfade;
    int count;
    void Start()
    {
       count=0;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    
    public async void Skip()
    {
        count++;

        if (count ==1)
        {
            buttonfade.DOFade(1, 1f).SetUpdate(true);
        }
        else if (count >=2)
        {
        await fade.DOFade(1, 2f).SetUpdate(true).AsyncWaitForCompletion();
        SceneManager.LoadScene(Name); 
        }
        
    }
    
}