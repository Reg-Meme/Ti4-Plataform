using UnityEngine;

public class SoundtrackManager : MonoBehaviour
{
    public static SoundtrackManager instance;
    public void Awake()
    {
        if(instance == null)
        {
            instance=this;
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
