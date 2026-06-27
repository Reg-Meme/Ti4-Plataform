using UnityEngine;
using Unity.Cinemachine;

public class AimCameraPosition : MonoBehaviour
{
    private CinemachineDeoccluder deoccluder;

    private void Awake()
    {
        deoccluder = GetComponent<CinemachineDeoccluder>();
    }

    private void OnEnable()
    {
        if (deoccluder != null) deoccluder.enabled = false;
    }

    private void OnDisable()
    {
        if (deoccluder != null) deoccluder.enabled = true;
    }
}