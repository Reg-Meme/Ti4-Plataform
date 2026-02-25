using UnityEngine;

public class LeoSpineLimits : MonoBehaviour
{
    public float X = 5f;
    public float Y = 2f;
    public float Z = 5f;

    private Vector3 StartPos;

    void Start()
    {
 
        StartPos = transform.localPosition;
    }

    void Update()
    {
        Vector3 pos = transform.localPosition;
        pos.x = Mathf.Clamp(pos.x, StartPos.x - X, StartPos.x + X);
        pos.y = Mathf.Clamp(pos.y, StartPos.y - Y, StartPos.y + Y);
        pos.z = Mathf.Clamp(pos.z, StartPos.z - Z, StartPos.z + Z);

        transform.localPosition = pos;
    }

    //Gizmos do Gepeto
    private void OnDrawGizmos()
    {

        if (transform.parent != null)
        Gizmos.matrix = transform.parent.localToWorldMatrix;
        Vector3 centroGizmo = Application.isPlaying ? StartPos : transform.localPosition;
        Vector3 tamanhoGizmo = new Vector3(X * 2, Y * 2, Z * 2);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(centroGizmo, tamanhoGizmo);
        Gizmos.color = new Color(0, 1, 1, 0.05f);
        Gizmos.DrawCube(centroGizmo, tamanhoGizmo);
    }
}
