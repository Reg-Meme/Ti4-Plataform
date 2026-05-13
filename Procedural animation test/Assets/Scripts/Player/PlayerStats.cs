using UnityEngine;
public class PlayerStats
{
    public static bool iddle = true;
    public static bool bottleMode;
    public static bool bladeMode;
    public static bool isJumpig;

    public static float Timer(float t)
    {
        Debug.Log("timer: " + t);
        t -= Time.deltaTime;
        return t;
    }
}