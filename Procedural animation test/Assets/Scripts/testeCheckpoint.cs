using UnityEngine;

public class testeCheckpoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PlayerStats.haveCheckPoint)
        {
        transform.position = PlayerStats.checkPointPosition;
    
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
