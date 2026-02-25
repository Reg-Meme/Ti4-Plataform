using UnityEngine;

public class BasicIkFootSolver : MonoBehaviour
{

    public LayerMask groundLayer;
    public float RayDis = 1.5f;
    public float footOffset = 0.1f;
    public float Spd = 10f;

    private Vector3 CurrentPos, newPos, oldPos;
    private Vector3 OldPos, NewPos;
    private float Lerp;

    void Start()
    {

        CurrentPos = oldPos = newPos = transform.position;
        OldPos = NewPos = Vector3.up;
        Lerp = 1;
    }

    void Update()
    {
        FootNature();
    }

    void FootNature()
    {
        Ray ray = new Ray(transform.parent.position + Vector3.up, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, RayDis, groundLayer))
        {
           
            if (Vector3.Distance(newPos, hit.point) > 0.05f)
            {
                Lerp = 0;
                OldPos = CurrentPos;
                NewPos = hit.point;
            }
        }
        if (Lerp < 1)
        {
            Lerp += Time.deltaTime * Spd;
            CurrentPos = Vector3.Lerp(oldPos, newPos, Lerp);
            OldPos = Vector3.Lerp(OldPos, NewPos, Lerp);
        }
        transform.position = CurrentPos;
    }
}
