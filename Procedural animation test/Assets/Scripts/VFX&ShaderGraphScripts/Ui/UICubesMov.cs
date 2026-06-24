using UnityEngine;

public class UICubesMov : MonoBehaviour
{
    public float Ray;
    public float Spd;
    public enum Eixo { XY, XZ, YZ }
    public Eixo MainSpin;
    public float SubRay;
    public float Spd2;
    public Eixo SecSpin;
    public float RotSpd ;
    private float Angle;
    private float SubAngle;
    private Vector3 InitialPos;

    void Start()
    {
        InitialPos = transform.position;
    }

    void Update()
    {
        Angle += Spd * Time.deltaTime;
        Vector3 offset = CalcularOffset(Angle, Ray, MainSpin);
        SubAngle += Spd2 * Time.deltaTime;
        Vector3 SubOffset = CalcularOffset(SubAngle, SubRay, SecSpin);
        transform.position = InitialPos+ offset + SubOffset;
        transform.Rotate(new Vector3(1,0,1), RotSpd * -1 * Time.deltaTime);
    }

    Vector3 CalcularOffset(float Tim, float r, Eixo eixo)
    {
        float cos = Mathf.Cos(Tim) * r;
        float sin = Mathf.Sin(Tim) * r;

        switch (eixo)
        {
            case Eixo.XY: return new Vector3(cos, sin, 0);
            case Eixo.XZ: return new Vector3(cos, 0, sin);
            case Eixo.YZ: return new Vector3(0, cos, sin);
            default: return Vector3.zero;
        }
    }
}
