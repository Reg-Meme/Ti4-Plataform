using UnityEngine;
using UnityEngine.InputSystem;

public abstract class  Mechanics 
{
    protected BatterySystem battery;

    public float batteryCost = 10f;

    public virtual void Initialize (BatterySystem batterySystem)
    {
        battery = batterySystem;
    }

    public abstract void AttackButton();
    public virtual void UpdateState(Vector2 v2) {}
    public virtual void Tick() { }
    public virtual void FixedTick() { }
    public abstract void AimButton();
    public abstract void ReleaseAim();
     
}
