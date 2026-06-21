using UnityEngine;
using System.IO;
[System.Serializable]
public class PlayerData 
{
    public bool grabUnlock;
    public bool cutUnlock;
    public  Vector3 checkPointPosition;
    public  bool haveCheckPoint = false;
    public string lastScene;

    public PlayerData(bool grabUnlock, bool cutUnlock, Vector3 checkPointPosition, bool haveCheckPoint, string lastScene)
    {
        this.grabUnlock = grabUnlock;
        this.cutUnlock = cutUnlock;
        this.checkPointPosition = checkPointPosition;
        this.haveCheckPoint = haveCheckPoint;
        this.lastScene = lastScene;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
}
