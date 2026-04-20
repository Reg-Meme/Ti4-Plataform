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
        battery = GetComponent<BatterySystem>();
        Slash = new SlashMechanic(cutPlane, slashCam, aimSlashCam, crossSectionMat, slashLayerMask);
        Grab = new PhysicsGrab(Camera.main.transform, grabLayerMask, grabConfig, grabPoint, overheadPoint, highlightMaterial);
        mechanics = Slash;
        AniRef = GetComponent<AniManager>();
        mechanics.Initialize(battery);
    
        InputInfo.OnLockEvent += LockEvent;
        InputInfo.OnTradeEvent += Trade;

        AssignInputs();
        
    }
    void LockEvent(Vector2 v2)
    {
        axisInput = v2;
    }
    void Trade()
    {
        if (trade) mechanics = Slash;

        else mechanics = Grab;

        mechanics.Initialize(battery);

        inputInfo.ClearMechanicsEvent();
        AssignInputs();

        trade = !trade;
        Debug.Log("trade:" + trade);
    }
    void AssignInputs()
    {
        InputInfo.OnAttackEvent += () => { Debug.Log("ATAQUE CHEGOU NO USE"); mechanics.AttackButton();AniRef.FlipRef();};
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
}
