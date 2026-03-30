using System;
using UnityEngine;
using UnityEditor;
public static class GameBroadcast
{
    public static event Action OnPlayerAprouch;
    public static event Action<Vector3> OnCheckPointSave;

    public static void TriggerPlayerAprouch()
    {
        OnPlayerAprouch?.Invoke();
    }
    public static void CheckPointSave(Vector3 v3)
    {
        OnCheckPointSave?.Invoke(v3);
    }
}