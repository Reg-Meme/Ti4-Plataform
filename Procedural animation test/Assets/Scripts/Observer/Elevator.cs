using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float max;

    Vector3 total, startPosition;
    Vector3  b;
    public float speed = 0.2f;
    public float timer = 0.2f;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        total = transform.position;
        startPosition = transform.position;
       
        b = new Vector3(transform.position.x, max,  transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(Moviment.moviment.isAssGrounded())
        {
        timer = .2f;    
        Up();
        } 

        else
        {
            timer -= Time.deltaTime;
            if(timer < 0)Down();
        }
    }
    void Up()
    {
       
        total = Vector3.MoveTowards(transform.position, b, Time.deltaTime * speed);
       
           transform.position = total;

        
    }
    void Down()
    {
         total = Vector3.MoveTowards(transform.position, startPosition, Time.deltaTime * speed);
         
           transform.position = total;
    }
}
