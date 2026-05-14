using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
public class UiControl : MonoBehaviour
{
    PlayerInput Input;
    Inputs inputs;
    [SerializeField] private InputInfo inputInfo;

    [SerializeField] InputActionReference Menu;
    public bool MenuUi;
    public GameObject MenuCanvas;
    [SerializeField] GameObject CurrentButton;
    public UiAni MenuAni;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    void Start()
    {
        MenuCanvas.SetActive(false);
        // Input = GetComponent<PlayerInput>();
        // Input.actions.FindActionMap("Global").Enable();
        InputInfo.OnMenuEvent += OpenMenu;
    }


    public void OpenMenu()
    {
        Debug.Log("pause");
        MenuUi = !MenuCanvas.activeSelf;

        if (MenuUi)
        {
            
            inputInfo.SetUi();
            MenuCanvas.SetActive(true);
            MenuAni.UIIntro();
            Time.timeScale = 0;
            EventSystem.current.SetSelectedGameObject(CurrentButton);
        }
        else
        {
            inputInfo.SetGameplay();
            MenuAni.UIountro();
            Time.timeScale = 1;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }


    void Update()
    {
        // if(inputs.Global.Menu.triggered)
        // {

        //  OpenMenu();
        // }
        //Debug.Log(Input.currentActionMap);
    }
}
