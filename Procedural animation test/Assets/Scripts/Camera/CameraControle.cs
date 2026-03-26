using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    
    void Start() // alnha e locka o mouse
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }


}