using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class UiControl : MonoBehaviour
{
    PlayerInput Input;
    [SerializeField] InputActionReference Menu;
    public bool MenuUi;
    public GameObject MenuCanvas;
    [SerializeField]  GameObject CurrentButton;
    public UiAni MenuAni;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MenuCanvas.SetActive(false);
        Input = GetComponent<PlayerInput>();
        Input.actions.FindActionMap("Global").Enable();
    }
    public void OnEnable()
    {
        Menu.action.started += OpenMenu;
    }

    public void OnDisable()
    {
        Menu.action.started -= OpenMenu;
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {
        MenuUi = !MenuCanvas.activeSelf;

        if (MenuUi)
        {
            Input.SwitchCurrentActionMap("UI");
            MenuCanvas.SetActive(true);
            MenuAni.UIIntro();
            Time.timeScale = 0;
            EventSystem.current.SetSelectedGameObject(CurrentButton);
        }
        else
        {
            Input.SwitchCurrentActionMap("Player");
            MenuAni.UIountro();
            Time.timeScale = 1;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
    
    void Update()
    {
        //Debug.Log(Input.currentActionMap);
    }
}
