using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraTriggers : MonoBehaviour
{
    [Header("Config")]
    public bool oneShot = false;

    [Tooltip("Tag do objeto que ativa a trigger")]
    public string collisionTag = "Player";

    [Header("Timer")]
    public bool useTimer = false;
    public float timerSeconds = 3f;

    [Header("Cameras")]
    public CinemachineCamera enterCamera;
    public CinemachineCamera exitCamera;

    private bool alreadyEntered = false;
    private bool alreadyExited = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (alreadyEntered) return;

        if (!string.IsNullOrEmpty(collisionTag) &&!collision.CompareTag(collisionTag)) return;

        if (useTimer)
        {
            StartCoroutine(CameraTimer());
        }
        else
        {
            CameraManeger.SwitchCamera(enterCamera);
        }

        if (oneShot)
        {
            alreadyEntered = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (useTimer) return;

        if (alreadyExited) return;

        if (!string.IsNullOrEmpty(collisionTag) && !collision.CompareTag(collisionTag)) return;

        CameraManeger.SwitchCamera(exitCamera);

        if (oneShot)
        {
            alreadyExited = true;
        }
    }

    private IEnumerator CameraTimer()
    {
        CameraManeger.SwitchCamera(enterCamera);

        yield return new WaitForSeconds(timerSeconds);

        CameraManeger.SwitchCamera(exitCamera);
    }
}