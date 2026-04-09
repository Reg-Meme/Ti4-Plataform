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
    public GameObject cutPlane;
    public GameObject slashCam;
    public GameObject aimSlashCam;
    public Material crossSectionMat;
    [SerializeField] LayerMask grabLayerMask;
    [SerializeField] LayerMask slashLayerMask;

    Vector2 axisInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //InputInfo.inputInfo.Initialize();

        battery = GetComponent<BatterySystem>();

        mechanics = new SlashMechanic(cutPlane, slashCam, aimSlashCam, crossSectionMat, slashLayerMask);

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
        if (trade) mechanics = mechanics = new SlashMechanic(cutPlane, slashCam, aimSlashCam, crossSectionMat, slashLayerMask);

        else mechanics = new PhysicsGrab(Camera.main.transform, grabLayerMask, grabConfig, grabPoint, overheadPoint);

        inputInfo.ClearMechanicsEvent();
        AssignInputs();
        trade = !trade;
        Debug.Log("trade:" + trade);
    }
    void AssignInputs()
    {
        InputInfo.OnAttackEvent += () => { Debug.Log("ATAQUE CHEGOU NO USE"); mechanics.AttackButton(); };
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
