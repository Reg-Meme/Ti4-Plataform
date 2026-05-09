using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Saver;

    void Awake()
    {
        if(Saver == null)
        {
            Saver = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
