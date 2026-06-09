using UnityEngine;

using Unity.Cinemachine;

public class CameraZone : MonoBehaviour
{
    public CinemachineFreeLook freeLook;

    public float corridorRadius = 2f;
    public float smoothSpeed = 5f;

    private float targetRadius;

    private float topOriginal;
    private float middleOriginal;
    private float bottomOriginal;

    private bool inCorridor;

    private void Start()
    {
        topOriginal = freeLook.m_Orbits[0].m_Radius;
        middleOriginal = freeLook.m_Orbits[1].m_Radius;
        bottomOriginal = freeLook.m_Orbits[2].m_Radius;

        targetRadius = middleOriginal;
    }

    private void Update()
    {
        float currentTop = freeLook.m_Orbits[0].m_Radius;
        float currentMid = freeLook.m_Orbits[1].m_Radius;
        float currentBottom = freeLook.m_Orbits[2].m_Radius;

        float targetTop = inCorridor ? corridorRadius : topOriginal;
        float targetMid = inCorridor ? corridorRadius : middleOriginal;
        float targetBottom = inCorridor ? corridorRadius : bottomOriginal;

        freeLook.m_Orbits[0].m_Radius = Mathf.Lerp(currentTop, targetTop, smoothSpeed * Time.deltaTime);

        freeLook.m_Orbits[1].m_Radius = Mathf.Lerp(currentMid, targetMid, smoothSpeed * Time.deltaTime);

        freeLook.m_Orbits[2].m_Radius = Mathf.Lerp(currentBottom, targetBottom, smoothSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) inCorridor = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) inCorridor = false;
    }
}