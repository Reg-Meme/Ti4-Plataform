using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class MainMenuControl : MonoBehaviour
{
    PlayerInput Input;
    Inputs inputs;
    [SerializeField] private InputInfo inputInfo;

    public GameObject MenuCanvas;
    [SerializeField] GameObject CurrentButton;
    public MainMenuAni MenuAni;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    void Start()
    {
        InputInfo.OnMenuEvent += OpenMenu;
        
    }


    public void OpenMenu()
    {
          inputInfo.SetUi();
        EventSystem.current.SetSelectedGameObject(CurrentButton);
        Input.SwitchCurrentActionMap("UI");  
    }


    void Update()
    {
        
    }
}
