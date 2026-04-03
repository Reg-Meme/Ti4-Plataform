using UnityEngine;

public class BatteryOrb : MonoBehaviour
{
    public float rechargeAmount = 25f;

    void OnTriggerEnter(Collider other)
    {
        BatterySystem battery = other.GetComponentInParent<BatterySystem>();

        if (battery != null)
        {
            battery.Recharge(rechargeAmount);
            Destroy(gameObject);
        }
    }
}