using Unity.VisualScripting;
using UnityEngine;

public class CheckpointCutEvent : MonoBehaviour
{
    public GameObject[] Visual;
    bool takePoint = false;
    public Transform Checkpoint;
    
    public void OnDisable()
    {
        Visual[0].SetActive(true);
        Visual[1].SetActive(true);
        Visual[2].SetActive(true);
        Visual[3].SetActive(true);
        if (!takePoint)
        {
         takePoint = true;
         GameBroadcast.CheckPointSave(Checkpoint.position);   
        }
                
    }
}
