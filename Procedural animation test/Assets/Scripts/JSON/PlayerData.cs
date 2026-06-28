using UnityEngine;
using System.IO;
using NUnit.Framework;
using System.Collections.Generic;
[System.Serializable]
public class PlayerData 
{
    public bool grabUnlock;
    public bool cutUnlock;
    public  Vector3 checkPointPosition;
    public  bool haveCheckPoint = false;
    public string lastScene;
    public List<string> collectedItems = new List<string>();
    public int totalCollectables = 0;
    public AudioClip clip;
    private float musicTime = 0;

    public PlayerData(bool grabUnlock, bool cutUnlock, Vector3 checkPointPosition, bool haveCheckPoint, string lastScene, AudioClip clip, float musicTime)
    {
        this.grabUnlock = grabUnlock;
        this.cutUnlock = cutUnlock;
        this.checkPointPosition = checkPointPosition;
        this.haveCheckPoint = haveCheckPoint;
        this.lastScene = lastScene;
        this.collectedItems = new List<string>();
        this.clip = clip;
        this.musicTime = musicTime;

    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
}
