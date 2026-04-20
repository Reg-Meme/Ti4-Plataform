using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem.Users;
public static class GameBroadcast
{
    public static event Action OnPlayerAprouch;
    public static event Action<Vector3> OnCheckPointSave;
    public static event Action<string> OnInputSchemaChange; 

    public static void TriggerPlayerAprouch()
    {
        OnPlayerAprouch?.Invoke();
    }
    public static void CheckPointSave(Vector3 v3)
    {
        OnCheckPointSave?.Invoke(v3);
    }
  
}