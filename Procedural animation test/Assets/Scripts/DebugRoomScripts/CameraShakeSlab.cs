using Unity.VisualScripting;
using UnityEngine;
using Unity.Cinemachine;
public class CameraShakeSlab : MonoBehaviour
{
    public float dur;
    public float freq;
    public float amp;
   // public CinemachineCamera CinCam;
   // private CinemachineBasicMultiChannelPerlin CamShake;

    void Start()
    {
      //  CamShake = CinCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           CameraShakeManager.Shaker.ShakePulse(amp,freq,dur);
        }
    }
}
