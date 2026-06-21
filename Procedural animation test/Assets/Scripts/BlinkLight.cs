using UnityEngine;

public class BlinkLight : MonoBehaviour
{
    public float blinkSpeed = 0.5f;

    private Light lamp;

    void Start()
    {
        lamp = GetComponent<Light>();
        InvokeRepeating(nameof(ToggleLight), 0f, blinkSpeed);
    }

    void ToggleLight()
    {
        lamp.enabled = !lamp.enabled;
    }
}