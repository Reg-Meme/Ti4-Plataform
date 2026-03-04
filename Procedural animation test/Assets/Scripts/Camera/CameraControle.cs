using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public Transform cameraTarget;
    public InputActionReference lookAction;

    [Header("Sensibilidade")]
    public float mouseSensitivity = 0.1f;
    public float controllerSensitivity = 100f;

    [Header("Limites")]
    public float minVerticalAngle = -30f;
    public float maxVerticalAngle = 60f;

    private float yaw;
    private float pitch;
    private Moviment mov;

    void Start() // alnha e locka o mouse
    {
        mov = player.GetComponentInParent<Moviment>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void Update() // coloca rotacao 
    {
        if (cameraTarget == null || lookAction == null)
            return;

        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();

        bool isMouse = Mouse.current != null && Mouse.current.delta.ReadValue() != Vector2.zero;
        float sensitivity = isMouse ? mouseSensitivity : controllerSensitivity * Time.deltaTime;

        yaw += lookInput.x * sensitivity;
        pitch -= lookInput.y * sensitivity;
        pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);

        cameraTarget.rotation = Quaternion.Euler(pitch, yaw, 0);

        if (mov != null && !mov.BottleMode && mov.currentInput.magnitude > 0.1f)
        {
            Rigidbody rb = mov.GetComponent<Rigidbody>();

            if (rb != null && !mov.BottleMode && mov.currentInput.magnitude > 0.1f)
            {
                Quaternion targetRot = Quaternion.Euler(0, yaw, 0);
                rb.MoveRotation(targetRot);
            }
        }
    }
}