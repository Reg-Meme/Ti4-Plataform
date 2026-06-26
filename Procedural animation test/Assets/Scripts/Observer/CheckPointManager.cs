using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(PlayerStats.checkPointPosition != newPosition)
        {
        PlayerStats.checkPointPosition = newPosition;
        PlayerStats.haveCheckPoint = true;
        PlayerStats.lastScene = SceneManager.GetActiveScene().name;
        PlayerStats.SaveStats();
        Debug.Log("Checkpoint Salvo em: " + newPosition + PlayerStats.haveCheckPoint);
            
        }

    }
    
   
}
