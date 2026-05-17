using UnityEngine;
using System.IO;
[System.Serializable]
public class PlayerData 
{
    public bool grabUnlock;
    public bool cutUnlock;
    public  Vector3 checkPointPosition;
    public  bool haveCheckPoint = false;

    public PlayerData(bool grabUnlock, bool cutUnlock, Vector3 checkPointPosition, bool haveCheckPoint)
    {
        this.grabUnlock = grabUnlock;
        this.cutUnlock = cutUnlock;
        this.checkPointPosition = checkPointPosition;
        this.haveCheckPoint = haveCheckPoint;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
}
