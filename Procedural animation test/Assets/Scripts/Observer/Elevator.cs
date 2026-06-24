using System.Collections;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float max;

    Vector3 total, startPosition;
    Vector3 b;
    public float speed = 0.2f;
    public float timer = 0.2f;
    public Vector3 size;
    public Vector3 offset;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        total = transform.position;
        startPosition = transform.position;

        b = new Vector3(transform.position.x, max, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        

            if (PlayerInContact())
            {
                timer = .2f;
                if(ElevatorTouching()) return;
                Up();
            }

            else
            {
                timer -= Time.deltaTime;
                if (timer < 0) Down();
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
    bool PlayerInContact()
    {
      if(Physics.CheckBox(transform.position + offset,size/2, Quaternion.identity, LayerMask.GetMask("Player"))) 
      return true;
      else return false;
    }
    bool ElevatorTouching()
    {
    if(Physics.CheckBox(transform.position + offset,size, Quaternion.identity, LayerMask.GetMask("Cut"))) 
      return true;
      else return false;
    }

public void OnDrawGizmosSelected()
    {
        if (PlayerInContact())

        Gizmos.color = Color.green;
        else
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position + offset, size);
    }
}
