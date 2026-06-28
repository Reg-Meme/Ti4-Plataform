using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatManager : MonoBehaviour
{
    public static CheatManager Instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        Debug.Log("entrei no start");
        InputInfo.LevelChange += ChangeScene;
        InputInfo.LevelChange1 += ChangeScene1;
        InputInfo.LevelChange2 += ChangeScene2;
        InputInfo.LevelChange3 += ChangeScene3;

    }

    void ChangeScene()
    {
        PlayerStats.haveCheckPoint = false;
        PlayerStats.SaveStats();
        SceneManager.LoadScene("1Level");
    }
    void ChangeScene1()
    {
        PlayerStats.haveCheckPoint = false;
        PlayerStats.SaveStats();
        SceneManager.LoadScene("2Level New");
    }
    void ChangeScene2()
    {
        PlayerStats.haveCheckPoint = false;
        PlayerStats.SaveStats();
        SceneManager.LoadScene("LevelG");
    }
    void ChangeScene3()
    {
     
        PlayerStats.haveCheckPoint = false;
        PlayerStats.SaveStats();
        SceneManager.LoadScene("BossLevel");
    }
}
