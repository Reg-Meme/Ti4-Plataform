using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static Vector3 checkPointPosition;
   
    public static bool haveCheckPoint = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GameBroadcast.OnCheckPointSave += SavePosition;
    }
    void OnDisable()
    {
        GameBroadcast.OnCheckPointSave -= SavePosition;
    }
    public static void SavePosition(Vector3 newPosition)
    {
        checkPointPosition = newPosition;
        haveCheckPoint = true;
        Debug.Log("Checkpoint Salvo em: " + newPosition);
    }
    
   
}
