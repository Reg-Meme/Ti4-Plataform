using UnityEngine;

public abstract class Move 
{
    public abstract void Movimentation(Vector2 input, Rigidbody rb, float maxSpeed);
}
