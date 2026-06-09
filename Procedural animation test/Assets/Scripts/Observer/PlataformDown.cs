using System.Collections;
using UnityEngine;

public class PlataformDown : MonoBehaviour
{Rigidbody rb;
public float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
  
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        StartCoroutine("Timer");
        
            
        } 
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);

rb.isKinematic = false;
        
    }
}
