using UnityEngine;

[CreateAssetMenu(fileName = "FieldOfViewData", menuName = "Scriptable Objects/FieldOfViewData")]
public class FieldOfViewData : ScriptableObject
{
    public float radius;
    [Range(0,360)]
    public float angle;
    public LayerMask player;
    public LayerMask obstacles;
    public bool canSeePlayer;
    public GameObject playerObj;
    
}
