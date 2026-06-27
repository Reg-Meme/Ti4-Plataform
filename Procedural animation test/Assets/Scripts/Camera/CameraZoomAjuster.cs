using UnityEngine;
using Unity.Cinemachine;

public class CameraZoomAdjuster : MonoBehaviour
{
    [Header("Referências")]
    public CinemachineCamera cinemachineCamera;
    public Transform cameraTarget;

    [Header("Layers que colidem")]
    public LayerMask collisionLayers;

    [Header("Zoom")]
    public float minDistance = 0.3f;
    public float maxDistance = 2f;
    public float adjustSpeed = 8f;
    public float sphereRadius = 0.2f; // tamanho da esfera do cast

    private CinemachineOrbitalFollow orbitalFollow;

    private void Start()
    {
        orbitalFollow = cinemachineCamera.GetComponent<CinemachineOrbitalFollow>();
    }

    private void Update()
    {
        if (orbitalFollow == null || cameraTarget == null) return;

        // Direção do target até onde a câmera estaria no máximo
        Vector3 directionToCamera = (transform.position - cameraTarget.position).normalized;

        float desiredDistance = maxDistance;

        // SphereCast é mais robusto que Raycast pra câmera — pega colisões laterais também
        if (Physics.SphereCast(
            cameraTarget.position,
            sphereRadius,
            directionToCamera,
            out RaycastHit hit,
            maxDistance,
            collisionLayers))
        {
            // Subtrai o raio pra câmera não encostar na parede
            desiredDistance = Mathf.Clamp(hit.distance - sphereRadius, minDistance, maxDistance);
        }

        orbitalFollow.RadialAxis.Value = Mathf.Lerp(
            orbitalFollow.RadialAxis.Value,
            desiredDistance,
            Time.deltaTime * adjustSpeed
        );
    }
}