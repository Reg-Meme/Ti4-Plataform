using UnityEngine;
using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class UiAni : MonoBehaviour
{
    public PlayerInput Input;
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
    
    public float BlocksTimer;
    float blocksTim;
    [SerializeField] RectTransform UpPartBlock;
    [SerializeField] CanvasGroup UpPartBlockCG;
    public Vector2 UpPartBlockStartAnchor;
    public Vector2 UpPartBlockFinalAnchor;
    [SerializeField] RectTransform DownPartBlock;
    [SerializeField] CanvasGroup DownPartBlockCG;
    public Vector2 DownPartBlockStartAnchor;
    public Vector2 DownPartBlockFinalAnchor;
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
    public GameObject CubesDeco;
    public GameObject CabesDeco;
    public GameObject CamUi;
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

    void Start()
    {
        blocksTim = BlocksTimer;
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
        CabesDeco.transform.DORotate(new Vector3(0,360, 0), RotationTime/2, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetRelative(true).SetEase(Ease.Linear).SetUpdate(true);
    }
    public void UIIntro()
    {
        // Dokills 
        UiCG.DOKill();
        UpPartBlockCG.DOKill();
        DownPartBlockCG.DOKill();
        ButtonsGroupCG.DOKill();
        UpPart.DOKill();
        DownPart.DOKill();
        UpPartBlock.DOKill();
        DownPartBlock.DOKill();
        ButtonsGroup.DOKill();
        RotatingCircleCG.DOKill();
        
        // 2. Reset de posição inicial (Instantâneo)
        UiCG.alpha = 0;
        UpPartBlockCG.alpha = 0;
        DownPartBlockCG.alpha = 0;
        ButtonsGroupCG.alpha = 0;
        UpPart.anchoredPosition = new Vector2(UpPart.anchoredPosition.x, UpPartStartAnchor);
        DownPart.anchoredPosition = new Vector2(DownPart.anchoredPosition.x, DownPartStartAnchor);
        UpPartBlock.anchoredPosition = UpPartBlockStartAnchor;
        DownPartBlock.anchoredPosition = DownPartBlockStartAnchor;
        ButtonsGroup.anchoredPosition = new Vector2(ButtonsGroup.anchoredPosition.x, ButtonsGroupStartAnchor);

        // 3. Sequência sincronizada
        Sequence intro = DOTween.Sequence().SetUpdate(true);
        CamUi.SetActive(true);

        // intro Fade
        intro.Join(UiCG.DOFade(1, 1));
        intro.Join(RotatingCircleCG.DOFade(1, RCFadeTime));

        // Bars
        intro.Join(UpPart.DOAnchorPosY(UpPartFinalAnchor, TweenDur));
        intro.Join(DownPart.DOAnchorPosY(DownPartFinalAnchor, TweenDur));

        // Using this way and not by the duration of the twin (for fixed duration)
        float fixedInsert = Mathf.Max(0, TweenDur - IntroOverlapTime);
        float fixedInsertTwo = Mathf.Max(0, TweenDur - IntroOverlapTime + 0.2f);

        // Blocks
        intro.Insert(fixedInsert, UpPartBlock.DORotate(new Vector3(0, 0, 90), BRotationime, RotateMode.FastBeyond360));
        intro.Insert(fixedInsert, UpPartBlock.DOAnchorPos(UpPartBlockFinalAnchor, 1).SetEase(Ease.OutQuad));
        intro.Insert(fixedInsert, UpPartBlockCG.DOFade(1, BFadeTime));

        intro.Insert(fixedInsert, DownPartBlock.DORotate(new Vector3(0, 0, 90), BRotationime, RotateMode.FastBeyond360));
        intro.Insert(fixedInsert, DownPartBlock.DOAnchorPos(DownPartBlockFinalAnchor, 1).SetEase(Ease.OutQuad));
        intro.Insert(fixedInsert, DownPartBlockCG.DOFade(1, BFadeTime));

        // Buttons
        intro.Insert(fixedInsertTwo,SchemesGroup.DOFade(1,BGFadeTime));
        intro.Insert(fixedInsertTwo, ButtonsGroup.DOAnchorPosY(ButtonsGroupFinalAnchor, TweenDur).SetEase(Ease.InOutCubic));
        intro.Insert(fixedInsert, ButtonsGroupCG.DOFade(1, BGFadeTime).SetEase(Ease.InFlash));
    }

    public void UIountro()
    {
        // Dokills 
        UpPartBlock.DOKill();
        UpPartBlockCG.DOKill();
        DownPartBlock.DOKill();
        DownPartBlockCG.DOKill();
        ButtonsGroup.DOKill();
        ButtonsGroupCG.DOKill();
        UpPart.DOKill();
        DownPart.DOKill();
        UiCG.DOKill();

        options = false;
        OptionsAni();

        Sequence outro = DOTween.Sequence().SetUpdate(true);

        // Blocks & Buttons 
        outro.Join(UpPartBlock.DORotate(new Vector3(0, 0, -90), BRotationime, RotateMode.FastBeyond360));
        outro.Join(UpPartBlock.DOAnchorPos(UpPartBlockStartAnchor, 1).SetEase(Ease.OutQuad));
        outro.Join(UpPartBlockCG.DOFade(0, BFadeTime));
        outro.Join(SchemesGroup.DOFade(0, BFadeTime));

        outro.Join(DownPartBlock.DORotate(new Vector3(0, 0, -90), BRotationime, RotateMode.FastBeyond360));
        outro.Join(DownPartBlock.DOAnchorPos(DownPartBlockStartAnchor, 1).SetEase(Ease.OutQuad));
        outro.Join(DownPartBlockCG.DOFade(0, BFadeTime));

        outro.Join(ButtonsGroup.DOAnchorPosY(ButtonsGroupStartAnchor, TweenDur).SetEase(Ease.InOutCubic));
        outro.Join(ButtonsGroupCG.DOFade(0, BGFadeTime).SetEase(Ease.InFlash));

        // Outro fade
        outro.Join(UiCG.DOFade(0, 1).SetEase(Ease.InFlash).OnComplete(() =>{ UI.SetActive(false);CamUi.SetActive(false);}));

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
        
        OptionsGroup.DOKill();
        OptionsBg.DOKill();
        Optionsicon.DOKill();
        OptionsTxT.DOKill();
        OptionsTxTCG.DOKill();
        OptionsiconCG.DOKill();
        CubesDeco.transform.DOKill();
        CabesDeco.transform.DOKill();
        if (options)
        {
            OptionsisAni = true;
            // Options intro
            OptionsTxT.DOScaleX(1, 0.3f).SetUpdate(true);
            Settings.SetActive(true);

            Sequence intro = DOTween.Sequence().SetUpdate(true);

            intro.Join(SchemesGroup.DOFade(0,0.5f));
            intro.Join(OptionsGroup.DOScaleY(OptionsGpSizeFinal, OptionsTweenDur));
            intro.Join(CabesDeco.transform.DOLocalMoveZ(-5.63f, TweenDur).SetEase(Ease.OutCubic));


            float fixedInsert = OptionsTweenDur - OptionsOverlapTime;
            float fixedInsertTwo = OptionsTweenDur - OptionsOverlapTimeTwo;
            
            intro.Insert(fixedInsert, OptionsBg.DOAnchorPosX(OptionsBgFinalAnchor, OptionsTweenDur));
            intro.Insert(fixedInsert, OptionsBg.DOScaleX(OptionsBgSizeFinalAnchor, OptionsTweenDur));
            intro.Insert(fixedInsertTwo,CubesDeco.transform.DOScale(1,OptionsTweenDur+1));
            intro.Insert(fixedInsertTwo, Optionsicon.DOAnchorPosY(OptionsIconFinalAnchor, OptionsTweenDur));
            intro.Insert(fixedInsertTwo, Optionsicon.DORotate(new Vector3(0, 0, 360), TweenDur, RotateMode.FastBeyond360).SetEase(Ease.OutCubic));
            intro.Insert(fixedInsert, OptionsiconCG.DOFade(1, OptionsTweenDur).SetEase(Ease.InFlash));
            intro.Insert(fixedInsertTwo, OptionsTxT.DOAnchorPosX(OptionsTxTFinalAnchor, OptionsTweenDur));
            intro.Insert(fixedInsert, OptionsTxTCG.DOFade(1, OptionsTweenDur).SetEase(Ease.InFlash));
            intro.OnComplete(() => OptionsisAni = false);
        }
        else
        {
            OptionsisAni = true;
            // Options Outro
            Sequence outro = DOTween.Sequence().SetUpdate(true).OnComplete(() => Settings.SetActive(false));
            outro.Join(CubesDeco.transform.DOScale(0,OptionsTweenDur).SetEase(Ease.OutFlash));
            outro.Join(OptionsBg.DOAnchorPosX(OptionsBgStartAnchor, OptionsTweenDur));
            outro.Join(OptionsBg.DOScaleX(OptionsBgSizeStartAnchor, OptionsTweenDur));
            outro.Join(CabesDeco.transform.DOLocalMoveZ(-4.13f, OptionsTweenDur).SetEase(Ease.InFlash));
            outro.Join(Optionsicon.DOAnchorPosY(OptionsIconStartAnchor, OptionsTweenDur));
            outro.Join(Optionsicon.DORotate(new Vector3(0, 0, -360), TweenDur, RotateMode.FastBeyond360).SetEase(Ease.OutCubic));
            outro.Join(OptionsiconCG.DOFade(0, 0.2f).SetEase(Ease.InFlash));
            outro.Join(OptionsTxT.DOAnchorPosX(OptionsTxTStartAnchor, OptionsTweenDur));
            outro.Join(OptionsTxTCG.DOFade(0, OptionsTweenDur).SetEase(Ease.InFlash));


            float fixedInsertOut = OptionsTweenDur - OptionsOverlapTime;
            outro.Insert(fixedInsertOut,CubesDeco.transform.DOScale(0,0.1f).SetEase(Ease.InCubic));
            outro.Insert(Mathf.Max(0, fixedInsertOut), OptionsGroup.DOScaleY(OptionsGpSizeStart, OptionsTweenDur));
            outro.Insert(fixedInsertOut,SchemesGroup.DOFade(1,BGFadeTime));
            outro.OnComplete(() => {OptionsisAni = false;});

        }
        
    }

    public void Resume()
    {
        Input.SwitchCurrentActionMap("Player");
        UIountro();
        Time.timeScale = 1;
        EventSystem.current.SetSelectedGameObject(null);
    }


}
