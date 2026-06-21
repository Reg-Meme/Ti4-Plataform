using UnityEngine;

public class testeCheckpoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(PlayerStats.haveCheckPoint);
        if (PlayerStats.haveCheckPoint)
        {
            Debug.Log("im here");
            transform.position = PlayerStats.checkPointPosition;

        }
    }

    // Update is called once per frame

}
