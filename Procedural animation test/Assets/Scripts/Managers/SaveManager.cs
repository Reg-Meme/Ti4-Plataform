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
}
