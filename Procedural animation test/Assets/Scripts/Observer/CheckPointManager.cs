using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GameBroadcast.OnCheckPointSave += SavePosition;
    }
    void OnDisable()
    {
        GameBroadcast.OnCheckPointSave -= SavePosition;
    }
    void Start()
    {
        GameBroadcast.OnCheckPointSave += SavePosition;
    } 
    public static void SavePosition(Vector3 newPosition)
    {
        PlayerStats.checkPointPosition = newPosition;
        PlayerStats.haveCheckPoint = true;
        Debug.Log("Checkpoint Salvo em: " + newPosition + PlayerStats.haveCheckPoint);

    }
    
   
}
