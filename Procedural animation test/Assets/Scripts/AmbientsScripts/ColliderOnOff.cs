using System;
using UnityEngine;

public class ColliderOnFor : MonoBehaviour
{
    public GameObject col;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
   

    // Update is called once per frame
    void Off()
    {
        col.SetActive(false);
    }
    void On()
    {
        col.SetActive(true);
    }
}
