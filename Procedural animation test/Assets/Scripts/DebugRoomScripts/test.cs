using UnityEngine;

public class Test : MonoBehaviour
{
    public float spd;
    public float ray;
    private float angle;

     void Update()
    {
        angle += spd * Time.deltaTime;

        Vector3 newpos = new Vector3(Mathf.Cos(angle) * ray, transform.position.y, Mathf.Sin(angle) * ray);
        transform.LookAt(newpos);
        
        transform.position = newpos;
    }
}