using UnityEngine;

public class BatterySystem : MonoBehaviour
{
    public float maxBattery = 100f;
    public float currentBattery;

    private void Awake()
    {
        currentBattery = maxBattery;
    }

    public bool HasBattery(float amount)
    {
        return currentBattery >= amount;
    }
    public bool Consume(float amount)
    {
        if (currentBattery < amount) { return false; }
        currentBattery -= amount; return true;
    }
    public void Recharge(float amount)
    {
        currentBattery += amount; currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);
    }
    public float GetNormalized()
    {
        return currentBattery / maxBattery;
    }
}
