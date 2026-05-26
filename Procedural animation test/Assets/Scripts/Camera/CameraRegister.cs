using Unity.Cinemachine;
using UnityEngine;

public class CameraRegister : MonoBehaviour
{
    private void OnEnable()
    {
        CameraManeger.Register(GetComponent<CinemachineCamera>());
    }

    private void OnDisable()
    {
        CameraManeger.Unregister(GetComponent<CinemachineCamera>());
    }
}