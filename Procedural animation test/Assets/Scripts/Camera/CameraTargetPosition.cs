using UnityEngine;

public class CameraTargetAdjuster : MonoBehaviour
{
    [Header("Camera Target")]
    public Transform cameraTarget;
    public float normalY = 0.5f;
    public float bottleModeY = 0.3f;
    public float adjustSpeed = 5f;

    private bool wasBottleMode;

    private void Update()
    {
        float targetY = PlayerStats.bottleMode ? bottleModeY : normalY;
        Vector3 pos = cameraTarget.localPosition;
        pos.y = Mathf.Lerp(pos.y, targetY, Time.deltaTime * adjustSpeed);
        cameraTarget.localPosition = pos;
    }
}