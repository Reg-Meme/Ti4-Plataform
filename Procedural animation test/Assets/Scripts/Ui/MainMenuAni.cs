using UnityEngine;
using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Users;
public class MainMenuAni : MonoBehaviour
{
    public PlayerInput Input;
    [SerializeField] private InputInfo inputInfo;
    [SerializeField] CanvasGroup UiCG;
    public GameObject UI;
    [SerializeField] RectTransform RotatingCircle;
    [SerializeField] CanvasGroup RotatingCircleCG;
    public float RotationTime;
    public float RCFadeTime;
    [SerializeField] RectTransform UpPart;
    public float UpPartStartAnchor;
    public float UpPartFinalAnchor;
    [SerializeField] RectTransform DownPart;
    public float DownPartStartAnchor;
    public float DownPartFinalAnchor;
    
    
    public float BRotationime;
    public float BFadeTime;
    public float TweenDur;
    public bool MenuActive;
    public float IntroOverlapTime;

    [SerializeField] RectTransform ButtonsGroup;
    [SerializeField] CanvasGroup ButtonsGroupCG;
    public float ButtonsGroupStartAnchor;
    public float ButtonsGroupFinalAnchor;
    public float BGFadeTime;

    //optionsTweens&Deco
    public bool options;
    [SerializeField] RectTransform OptionsGroup;
    [SerializeField] RectTransform OptionsBg;
    [SerializeField] RectTransform OptionsTxT;
    [SerializeField] CanvasGroup OptionsTxTCG;
    [SerializeField] RectTransform Optionsicon;
    [SerializeField] CanvasGroup OptionsiconCG;
    public GameObject Settings;
    public float OptionsGpSizeStart;
    public float OptionsGpSizeFinal;
    public float OptionsBgStartAnchor;
    public float OptionsBgFinalAnchor;
    public float OptionsBgSizeStartAnchor;
    public float OptionsBgSizeFinalAnchor;
    public float OptionsTxTStartAnchor;
    public float OptionsTxTFinalAnchor;
    public float OptionsIconStartAnchor;
    public float OptionsIconFinalAnchor;
    public float OptionsTweenDur;
    public float OptionsOverlapTime;
    public float OptionsOverlapTimeTwo;
    [SerializeField] CanvasGroup SchemesGroup;
    public GameObject KBCtrlscheme;
    public GameObject GPCtrlscheme;


    public GameObject safeexit;
    public CanvasGroup safeexitfade;
    public GameObject CurButton;
    public GameObject ExitButton;
    void Start()
    {
        safeexit.SetActive(false);
        Settings.SetActive(false);
    }
    void OnEnable()
    {
        Input.onControlsChanged += ControlsChanged;
        
        ControlSchemes(Input.currentControlScheme);
    }

    void OnDisable()
    {
        Input.onControlsChanged -= ControlsChanged;
    }

    void ControlsChanged(PlayerInput input)
    {
        ControlSchemes(input.currentControlScheme);
    }
 

    void ControlSchemes(string Scheme)
    {
        if (Scheme == "Gamepad")
        {
            KBCtrlscheme.SetActive(false);
            GPCtrlscheme.SetActive(true);
        }
        else
        {
            KBCtrlscheme.SetActive(true);
            GPCtrlscheme.SetActive(false);
        }
    }
    
    
    public void Update()
    {
        //olha, vou deixar comentado pq fui ver o RWM e os outros pra rever como fazer e fiquei todo confuso
        //tudo q for comentado entre nos pode ser em ptbr, mas se for para outros, escreva em inglês


        //UI BackGound Looping Elements
        RotatingCircle.DORotate(new Vector3(0, 0, 360), RotationTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetRelative(true).SetEase(Ease.Linear).SetUpdate(true);
       
    }
    public void UIIntro()
    {
        // Dokills 
        UiCG.DOKill();
      
        ButtonsGroupCG.DOKill();
        UpPart.DOKill();
        DownPart.DOKill();
       
        ButtonsGroup.DOKill();
        RotatingCircleCG.DOKill();
        
        // 2. Reset de posição inicial (Instantâneo)
        UiCG.alpha = 0;
       
        ButtonsGroupCG.alpha = 0;
        UpPart.anchoredPosition = new Vector2(UpPart.anchoredPosition.x, UpPartStartAnchor);
        DownPart.anchoredPosition = new Vector2(DownPart.anchoredPosition.x, DownPartStartAnchor);
      
        ButtonsGroup.anchoredPosition = new Vector2(ButtonsGroup.anchoredPosition.x, ButtonsGroupStartAnchor);

        // 3. Sequência sincronizada
        Sequence intro = DOTween.Sequence().SetUpdate(true);
      

        // intro Fade
        intro.Join(UiCG.DOFade(1, 1));
        intro.Join(RotatingCircleCG.DOFade(1, RCFadeTime));

        // Bars
        intro.Join(UpPart.DOAnchorPosY(UpPartFinalAnchor, TweenDur));
        intro.Join(DownPart.DOAnchorPosY(DownPartFinalAnchor, TweenDur));

        // Using this way and not by the duration of the twin (for fixed duration)
        float fixedInsert = Mathf.Max(0, TweenDur - IntroOverlapTime);
        float fixedInsertTwo = Mathf.Max(0, TweenDur - IntroOverlapTime + 0.2f);

       

        // Buttons
        intro.Insert(fixedInsertTwo,SchemesGroup.DOFade(1,BGFadeTime));
        intro.Insert(fixedInsertTwo, ButtonsGroup.DOAnchorPosY(ButtonsGroupFinalAnchor, TweenDur).SetEase(Ease.InOutCubic));
        intro.Insert(fixedInsert, ButtonsGroupCG.DOFade(1, BGFadeTime).SetEase(Ease.InFlash));
    }

    public void UIountro()
    {
        // Dokills 
        ButtonsGroup.DOKill();
        ButtonsGroupCG.DOKill();
        UpPart.DOKill();
        DownPart.DOKill();
        UiCG.DOKill();

        options = false;
        OptionsAni();

        Sequence outro = DOTween.Sequence().SetUpdate(true);

        // Blocks & Buttons 
        

        outro.Join(ButtonsGroup.DOAnchorPosY(ButtonsGroupStartAnchor, TweenDur).SetEase(Ease.InOutCubic));
        outro.Join(ButtonsGroupCG.DOFade(0, BGFadeTime).SetEase(Ease.InFlash));

        

        // Bars 
        float maxInitialDur = Mathf.Max(BRotationime, 1f);
        float fixedInsertOut = Mathf.Max(0, maxInitialDur - IntroOverlapTime);

        outro.Insert(fixedInsertOut, UpPart.DOAnchorPosY(UpPartStartAnchor, TweenDur));
        outro.Insert(fixedInsertOut, DownPart.DOAnchorPosY(DownPartStartAnchor, TweenDur));
    }
    private bool OptionsisAni = false;
    public void OptionsToggle()
    {
        if (OptionsisAni) return;
        options = !options;
    }
    public void OptionsAni()
    {
       Sequence intro = DOTween.Sequence().SetUpdate(true);
        Sequence outro = DOTween.Sequence().SetUpdate(true).OnComplete(() => Settings.SetActive(false));

        if (!options)
        {
            intro.Kill();
        }
        else
        {
            outro.Kill();
        
        }

        OptionsGroup.DOKill();
        OptionsBg.DOKill();
        Optionsicon.DOKill();
        OptionsTxT.DOKill();
        OptionsTxTCG.DOKill();
        OptionsiconCG.DOKill();
    

        if (options)
        {

            if (!Settings.activeSelf)
            {
                UiSfXManager.soundManager.PlaySound(UiSfXManager.SoundType.OpenSettings);
            }

            OptionsisAni = true;
            // Options intro
            OptionsTxT.DOScaleX(1, 0.3f).SetUpdate(true);
            Settings.SetActive(true);

            intro.Join(SchemesGroup.DOFade(0, 0.5f));
            intro.Join(OptionsGroup.DOScaleY(OptionsGpSizeFinal, OptionsTweenDur));
         

            float fixedInsert = OptionsTweenDur - OptionsOverlapTime;
            float fixedInsertTwo = OptionsTweenDur - OptionsOverlapTimeTwo;

            intro.Insert(fixedInsert, OptionsBg.DOAnchorPosX(OptionsBgFinalAnchor, OptionsTweenDur));
            intro.Insert(fixedInsert, OptionsBg.DOScaleX(OptionsBgSizeFinalAnchor, OptionsTweenDur));
         
            intro.Insert(fixedInsertTwo, Optionsicon.DOAnchorPosY(OptionsIconFinalAnchor, OptionsTweenDur));
            intro.Insert(fixedInsertTwo, Optionsicon.DORotate(new Vector3(0, 0, 360), TweenDur, RotateMode.FastBeyond360).SetEase(Ease.OutCubic));
            intro.Insert(fixedInsert, OptionsiconCG.DOFade(1, OptionsTweenDur).SetEase(Ease.InFlash));
            intro.Insert(fixedInsertTwo, OptionsTxT.DOAnchorPosX(OptionsTxTFinalAnchor, OptionsTweenDur));
            intro.Insert(fixedInsert, OptionsTxTCG.DOFade(1, OptionsTweenDur).SetEase(Ease.InFlash));
            intro.OnComplete(() => OptionsisAni = false);
        }
        else
        {

            if (Settings.activeSelf)
            {
                UiSfXManager.soundManager.PlaySound(UiSfXManager.SoundType.CloseSettings);
            }

            OptionsisAni = true;
            // Options Outro


            outro.Join(OptionsBg.DOAnchorPosX(OptionsBgStartAnchor, OptionsTweenDur));
            outro.Join(OptionsBg.DOScaleX(OptionsBgSizeStartAnchor, OptionsTweenDur));
        
            outro.Join(Optionsicon.DOAnchorPosY(OptionsIconStartAnchor, OptionsTweenDur));
            outro.Join(Optionsicon.DORotate(new Vector3(0, 0, -360), TweenDur, RotateMode.FastBeyond360).SetEase(Ease.OutCubic));
            outro.Join(OptionsiconCG.DOFade(0, 0.2f).SetEase(Ease.InFlash));
            outro.Join(OptionsTxT.DOAnchorPosX(OptionsTxTStartAnchor, OptionsTweenDur));
            outro.Join(OptionsTxTCG.DOFade(0, OptionsTweenDur).SetEase(Ease.InFlash));

            float fixedInsertOut = OptionsTweenDur - OptionsOverlapTime;

            outro.Insert(Mathf.Max(0, fixedInsertOut), OptionsGroup.DOScaleY(OptionsGpSizeStart, OptionsTweenDur));
            outro.Insert(fixedInsertOut, SchemesGroup.DOFade(1, BGFadeTime));
            outro.OnComplete(() =>
            {
                OptionsisAni = false;
                Settings.SetActive(false);
            });
        }
        
    }

    public void Resume()
    {
        inputInfo.SetGameplay();
        UIountro();
        Time.timeScale = 1;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void SafeExit()
    {
        safeexitfade.DOFade(1, OptionsTweenDur);
        safeexit.SetActive(true);
        EventSystem.current.SetSelectedGameObject(CurButton);
    }
    public async void No()
    {
        await  safeexitfade.DOFade(0, 1f).SetUpdate(true).AsyncWaitForCompletion();
        safeexit.SetActive(false);
        EventSystem.current.SetSelectedGameObject(ExitButton);
    }


}
