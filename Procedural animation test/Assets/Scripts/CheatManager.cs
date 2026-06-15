using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatManager : MonoBehaviour
{
    public static CheatManager Instance { get; private set;}
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);  
        Debug.Log("entrei no start");
         //InputInfo.LevelChange += ChangeScene;
        InputInfo.LevelChange1 += ChangeScene1;
        InputInfo.LevelChange2 += ChangeScene2;

    } 
    void Start()
    {
      
    }
    void ChangeScene()
    {
       
        SceneManager.LoadScene("GymRoom");
    }
    void ChangeScene1()
    {
        SceneManager.LoadScene("1Level");
    }
    void ChangeScene2()
    {
        SceneManager.LoadScene("LevelG");
    }
}
