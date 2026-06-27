using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;
public class PlayerStats
{
    public static bool iddle = true;
    public static bool bottleMode;

    public static float time = 0.0f;
    public static bool hitGround;

    public static bool bladeMode;
    public static bool GrabMode;
    public static bool isJumpig;
    public static bool cutUnlock = false;
    public static bool grabUnlock = false;
    public static Vector3 checkPointPosition;
    public static bool haveCheckPoint = false;
    public static bool IsDead;
    public static string lastScene;
    public static List<string> collectedItems = new List<string>();

    public static void Init(PlayerData data)
    {
        cutUnlock = data.cutUnlock;
        grabUnlock = data.grabUnlock;
        checkPointPosition = data.checkPointPosition;
        haveCheckPoint = data.haveCheckPoint;
        lastScene = data.lastScene;
        collectedItems = data.collectedItems ?? new List<string>();
    }
    public static void Del()
    {
        cutUnlock = false;
        grabUnlock = false;
        haveCheckPoint = false;
        collectedItems = new List<string>();
    }


    public static void SaveStats()
    {

        PlayerData data = new PlayerData(grabUnlock, cutUnlock, checkPointPosition, haveCheckPoint, lastScene);
        data.collectedItems = collectedItems;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/PlayerStats.json", json);
        Debug.Log(Application.persistentDataPath);
    }

    public static void AddCollectable(string itemName)
    {
        if (!collectedItems.Contains(itemName))
        {
            collectedItems.Add(itemName);
            SaveStats();
        }
    }
    public static PlayerData LoadStats()
    {
        string path = Application.persistentDataPath + "/PlayerStats.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            return data;
        }
        else return null;
    }
    public static void ClearStats()
    {
        string path = Application.persistentDataPath + "/PlayerStats.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Del();
        }
    }
}