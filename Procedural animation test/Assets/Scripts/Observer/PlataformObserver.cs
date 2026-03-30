using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlataformObserver : MonoBehaviour
{
    public float fallTime = 1;
  
    Rigidbody rb;
    public List<GameObject> plataforms;
    int i = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
 

    // Update is called once per frame
    void OnEnable()
    {
        GameBroadcast.OnPlayerAprouch += StartFall;
    }
    void OnDisable()
    {
        GameBroadcast.OnPlayerAprouch -= StartFall;
    }
    void StartFall()
    {
        StartCoroutine(Down());

    }
    IEnumerator Down()
    {

        if (i < plataforms.Count)
        {
            yield return new WaitForSeconds(fallTime);

            plataforms[i].GetComponent<Rigidbody>().isKinematic = false;
            Destroy(plataforms[i], 3f);
            i++;
             StartCoroutine(Down());
        }
        else
        
        plataforms.Clear();


    }
}
