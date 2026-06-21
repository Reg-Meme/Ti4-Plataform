using UnityEngine;

public class ControlBlinkLight : MonoBehaviour
{
    public float blinkSpeed = 0.5f;

    private Light lamp;

    [Header("Cores")]
    public Color normalColor = Color.white;
    public Color playerColor = Color.red;

    private bool activated = false;

    void Start()
    {
        lamp = GetComponent<Light>();

        InvokeRepeating(nameof(ToggleLight), 0f, blinkSpeed);
    }

    void ToggleLight()
    {
        lamp.enabled = !lamp.enabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;
            lamp.color = playerColor;
        }
    }
}