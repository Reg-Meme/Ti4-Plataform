using System;
using UnityEngine;

public class Use : MonoBehaviour
{
    Mechanics mechanics;
    bool trade = false;
    [SerializeField] private InputInfo inputInfo;
    public GameObject cutPlane;
    public GameObject slashCam;
    public GameObject aimSlashCam;
    public Material crossSectionMat;
    public LayerMask layerMask;
    Vector2 axisInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        mechanics = new SlashMechanic(cutPlane, slashCam, aimSlashCam, crossSectionMat, layerMask);
       InputInfo.OnLockEvent += LockEvent;
        InputInfo.OnTradeEvent += Trade;
       AssignInputs();
    }
    void LockEvent(Vector2 v2)
    {
        axisInput = v2;
    }
    void Trade()
    {   if(trade)
        mechanics =  mechanics = new SlashMechanic(cutPlane, slashCam, aimSlashCam, crossSectionMat, layerMask);

        else mechanics = new TesteMecanica();

        inputInfo.ClearMechanicsEvent();
        AssignInputs();
        trade = !trade;
        Debug.Log("trade:" + trade);
    }
    void AssignInputs()
    {
        InputInfo.OnAttackEvent += mechanics.AttackButton;
        InputInfo.OnAimEvent += mechanics.AimButton;
        InputInfo.OnReleaseAimEvent += mechanics.ReleaseAim;
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if(mechanics!= null)
        {
            mechanics.UpdateState(axisInput);
        }
    }
}
