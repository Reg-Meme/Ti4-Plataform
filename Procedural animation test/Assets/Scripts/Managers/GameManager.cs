using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] public InputInfo inputInfo;
    public static GameManager Instance;
    public string name;
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (Instance == null) Instance = this;
        else Destroy(this);
        inputInfo.Initialize();
        InputInfo.OnResetEvent += ResetLevel;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

  
    void ResetLevel()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
         PlayerStats.IsDead = false;
        //StartCoroutine(Reset());


    }
    IEnumerator reset()
    {
         AsyncOperation load = SceneManager.LoadSceneAsync("Gameplay", LoadSceneMode.Single);
         inputInfo.Initialize();

         yield return load;
         SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    }
}
