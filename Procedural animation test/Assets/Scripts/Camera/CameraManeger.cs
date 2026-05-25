using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManeger : MonoBehaviour
{
    public static List<CinemachineCamera> cameras =
        new List<CinemachineCamera>();

    public static CinemachineCamera ActiveCamera = null;

    public static bool IsActiveCamera(CinemachineCamera camera)
    {
        return camera == ActiveCamera;
    }

    public static void SwitchCamera(CinemachineCamera newCamera)
    {
        if (newCamera == null) return;

        ActiveCamera = newCamera;

        foreach (CinemachineCamera cam in cameras)
        {
            cam.Priority = (cam == newCamera) ? 10 : 0;
        }
    }

    public static void Register(CinemachineCamera camera)
    {
        if (!cameras.Contains(camera))
        {
            cameras.Add(camera);
        }
    }

    public static void Unregister(CinemachineCamera camera)
    {
        if (cameras.Contains(camera))
        {
            cameras.Remove(camera);
        }
    }
}