using UnityEngine;
using Unity.Cinemachine;


public class CamTestTP : MonoBehaviour
{
    public GameObject player;
    public float minDist = 0.3f;
    public CinemachineCamera Maincam;
    public CinemachineCamera otherCamera;
    void Start()
    {
        
    }
    void Update()
    {
        var check = player.transform.position - Maincam.transform.position;
    }

    public void ColiderCam ()
    {
        float check = 0;
        if (check <= minDist)
        {
            CameraManeger.SwitchCamera(otherCamera);
        }
    }
}
