using System;
using UnityEngine;

public class Use : MonoBehaviour
{

    BatterySystem battery;
    Mechanics mechanics;
    bool trade = false;
    [SerializeField] private InputInfo inputInfo;
    [SerializeField] PhysicsGrabConfig grabConfig;
    [SerializeField] Transform grabPoint;
    [SerializeField] Transform overheadPoint;
    [SerializeField] Material highlightMaterial;
    public GameObject cutPlane;
    public GameObject slashCam;
    public GameObject aimSlashCam;
    public Material crossSectionMat;
    [SerializeField] LayerMask grabLayerMask;
    [SerializeField] LayerMask slashLayerMask;
    AniManager AniRef;
    Vector2 axisInput;
    public static SlashMechanic Slash;
    public static PhysicsGrab Grab;


    void Awake()
    {
        if (PlayerStats.LoadStats() != null)
            PlayerStats.Init(PlayerStats.LoadStats());
        InputInfo.OnTradeEvent += Trade;
        InputInfo.OnLockEvent += LockEvent;
 battery = GetComponent<BatterySystem>();
        
    }
    void Start()
    {

        
        
        if (PlayerStats.grabUnlock)
        UnlockGrab();
        
        if (PlayerStats.cutUnlock) 
        UnlockCut();
      
    }

    void LockEvent(Vector2 v2)
    {
        axisInput = v2;
    }
    void Trade()
    {
        if (Grab == null || Slash == null) return;
        if (trade)
        {
            AniRef.Trade1Ani();
            mechanics = Slash;
           
        }
        else
        {
            AniRef.Trade2Ani();
            mechanics = Grab;
          
        }
        mechanics.Initialize(battery);

        inputInfo.ClearMechanicsEvent();
        AssignInputs();
        trade = !trade;
        Debug.Log("trade:" + trade);
    }
    void AssignInputs()
    {
        InputInfo.OnAttackEvent += () => { Debug.Log("ATAQUE CHEGOU NO USE"); mechanics.AttackButton(); if (PlayerStats.bladeMode) {; AniRef.BladeAni(); } };
        InputInfo.OnAimEvent += mechanics.AimButton;
        InputInfo.OnReleaseAimEvent += mechanics.ReleaseAim;
    }

    // Update is called once per frame
    void Update()
    {
        if (mechanics != null)
        {
            mechanics.UpdateState(axisInput);
            mechanics.Tick();
        }
    }

    void FixedUpdate()
    {
        if (mechanics != null)
        {
            mechanics.FixedTick();
        }
    }
    public void UnlockCut()
    {

        PlayerStats.cutUnlock = true;
        Slash = new SlashMechanic(cutPlane, slashCam, aimSlashCam, crossSectionMat, slashLayerMask);
        mechanics = Slash;
        AniRef = GetComponent<AniManager>();
        mechanics.Initialize(battery);
        inputInfo.ClearMechanicsEvent();
        AssignInputs();

    }
    public void UnlockGrab()
    {
        PlayerStats.grabUnlock = true;
        Grab = new PhysicsGrab(Camera.main.transform, grabLayerMask, grabConfig, grabPoint, overheadPoint, highlightMaterial);
        //if(PlayerStats.cutUnlock) return;
        mechanics = Grab;
        mechanics.Initialize(battery);
        inputInfo.ClearMechanicsEvent();
        AssignInputs();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UnlockCut"))
        {
            if (!PlayerStats.cutUnlock)
                UnlockCut();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("UnlockGrab"))
        {
            if (!PlayerStats.grabUnlock)
                UnlockGrab();
            Destroy(other.gameObject);
        }
    }
    [ContextMenu("Salvarr")]
    public void Test()
    {
        PlayerStats.SaveStats();

    }
    public void OnApplicationQuit()
    {
        PlayerStats.SaveStats();
    }
}
