using UnityEngine;
using UnityEngine.InputSystem;

public abstract class  Mechanics 
{
    public abstract void AttackButton();
    public virtual void UpdateState(Vector2 v2) {}
    public abstract void AimButton();
    public abstract void ReleaseAim();
     
}
