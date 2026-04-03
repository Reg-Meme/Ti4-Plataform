
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputInfo : MonoBehaviour, Inputs.IPlayerActions, Inputs.IGlobalActions
{
    public static InputInfo inputInfo { get; set; }

    Inputs input;

    //events
    public static event Action<Vector2> OnMoveEvent;
    public static event Action<Vector2> OnLockEvent;
    public static event Action OnInteractEvent;
    public static event Action OnMapEvent;
    public static event Action OnCrouchEvent;
    public static event Action OnCrouchReleaseEvent;
    public static event Action OnJumpEvent;
    public static event Action OnReleaseJumpEvent;
    public static event Action OnAttackEvent;
    public static event Action OnAimEvent;
    public static event Action OnResetEvent;
    public static event Action OnMenuEvent;
    public static event Action OnReleaseAimEvent;
    public static event Action OnTradeEvent;




    void Awake()
    {
        if (inputInfo == null) inputInfo = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        Initialize();
    }

    public void Initialize()
    {

        ClearEvents();
        if (input == null)
        {
            input = new Inputs();
            input.Player.SetCallbacks(this);
            input.Global.SetCallbacks(this);
        }
        SetGameplay();
        
    }
    private void ClearEvents()
    {
        OnMoveEvent = (v) => { };
        OnLockEvent = (v) => { };
        OnInteractEvent = () => { };
        OnMapEvent = () => { };
        OnCrouchEvent = () => { };
        OnJumpEvent = () => { };
        OnAttackEvent = () => { };
        OnAimEvent = () => { };
        OnResetEvent = () => { };
        OnMenuEvent = () => { };
        OnCrouchReleaseEvent = () => { };
        OnReleaseJumpEvent = () => { };
        OnReleaseAimEvent = () => { };
        OnTradeEvent = () => { };
    }
    public void ClearMechanicsEvent()
    {
        OnAttackEvent= () => { };
        OnAimEvent= () => { };
        OnReleaseAimEvent= () => { };
       
    }
    public void SetGameplay()
    {
        input.Player.Enable();
        input.Global.Enable();
        input.UI.Disable();
    }
    public void SetUi()
    {
        input.Player.Disable();
        input.UI.Enable();
    }

    #region PlayerAction
    public void OnMove(InputAction.CallbackContext context)
    {
       
        OnMoveEvent(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        OnLockEvent(context.ReadValue<Vector2>());
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
            OnInteractEvent();
    }
    public void OnMap(InputAction.CallbackContext context)
    {
        if (context.started)
            OnMapEvent();
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
            OnCrouchEvent();
        else if (context.canceled)
            OnCrouchReleaseEvent();

    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            OnJumpEvent();
        else if (context.canceled)
            OnReleaseJumpEvent();
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.started)
            OnAimEvent();
        else if (context.canceled)
            OnReleaseAimEvent();
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
            OnAttackEvent();
    }
    public void OnReset(InputAction.CallbackContext context)
    {
        if (context.started)
            OnResetEvent();
    }
    public void OnTrade(InputAction.CallbackContext context)
    {
        if (context.started)
            OnTradeEvent();
    }

    #endregion
    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.started)
            OnMenuEvent();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputInfo GetInputInfo()
    {
        return inputInfo;
    }
}
