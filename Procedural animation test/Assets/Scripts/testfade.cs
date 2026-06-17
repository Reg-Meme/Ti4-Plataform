using UnityEngine;
using DG.Tweening;
public class testfade : MonoBehaviour
{
    
    public CanvasGroup fade;
    void Start()
    {
        fade.DOFade(0,3f).SetUpdate(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
