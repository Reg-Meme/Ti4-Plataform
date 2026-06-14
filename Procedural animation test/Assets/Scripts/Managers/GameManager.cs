using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public InputInfo inputInfo;
    public static GameManager Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        inputInfo.Initialize();
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
