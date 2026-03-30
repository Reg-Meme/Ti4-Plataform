using UnityEngine;

public class TesteMecanica : Mechanics
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void AimButton()
    {
       Debug.Log("Estou mirando");
    }

    public override void AttackButton()
    {
       Debug.Log("Ataquei");
    }

    public override void ReleaseAim()
    {
        Debug.Log("Soltei a mira");
    }

   
}
