using UnityEngine;

public class FloatOnWater : MonoBehaviour
{
    public float amplitude = 0.2f;
    public float frequency = 1f;

    public float rotationAmount = 5f;
    public float rotationSpeed = 0.8f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Movimento vertical
        float y = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPos.x, y, startPos.z);

        // Balanço
        float rotZ = Mathf.Sin(Time.time * rotationSpeed) * rotationAmount;
        float rotX = Mathf.Cos(Time.time * rotationSpeed * 0.7f) * rotationAmount;

        transform.rotation = Quaternion.Euler(rotX, 0, rotZ);
    }
}