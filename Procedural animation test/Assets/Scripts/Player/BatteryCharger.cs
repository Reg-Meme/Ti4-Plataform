using UnityEngine;

public class BatteryCharger : MonoBehaviour
{
    public float baseChargeRate = 5f;
    public float maxChargeRate = 30f;
    public float accelerationTime = 4f;

    public Renderer padRenderer;
    public Color idleColor = Color.blue;
    public Color chargingColor = Color.cyan;

    public LineRenderer energyBeam;

    float idleTime;

    BatterySystem playerBattery;
    Rigidbody playerRb;

    void Start()
    {
        if (energyBeam != null)
            energyBeam.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        BatterySystem bat = other.GetComponentInParent<BatterySystem>();

        if (bat != null)
        {
            playerBattery = bat;
            playerRb = bat.GetComponent<Rigidbody>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        BatterySystem bat = other.GetComponentInParent<BatterySystem>();

        if (bat == playerBattery)
        {
            playerBattery = null;
            playerRb = null;
            idleTime = 0;

            if (energyBeam != null)
                energyBeam.enabled = false;
        }
    }

    void Update()
    {
        if (playerBattery == null || playerRb == null)
        {
            SetPadColor(idleColor);
            return;
        }

        bool playerIdle = playerRb.linearVelocity.magnitude < 0.2f;

        if (playerIdle)
        {
            idleTime += Time.deltaTime;

            float t = Mathf.Clamp01(idleTime / accelerationTime);
            float chargeRate = Mathf.Lerp(baseChargeRate, maxChargeRate, t);

            playerBattery.Recharge(chargeRate * Time.deltaTime);

            SetPadColor(Color.Lerp(idleColor, chargingColor, t));

            ActivateBeam();
        }
        else
        {
            idleTime = 0;
            SetPadColor(idleColor);

            if (energyBeam != null)energyBeam.enabled = false;
        }
    }

    void SetPadColor(Color color)
    {
        if (padRenderer != null)
        {
            padRenderer.material.SetColor("_EmissionColor", color * 2f);
        }
    }

    void ActivateBeam()
    {
        if (energyBeam == null) return;

        energyBeam.enabled = true;

        energyBeam.SetPosition(0, transform.position + Vector3.up * 0.1f);
        energyBeam.SetPosition(1, playerBattery.transform.position + Vector3.up * 1.5f);
    }
}