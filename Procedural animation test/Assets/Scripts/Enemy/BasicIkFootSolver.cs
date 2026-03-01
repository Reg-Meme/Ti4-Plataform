using UnityEngine;

public class BasicIkFootSolver : MonoBehaviour
{
    public Transform bodyTarget;
    public LayerMask groundLayer;
    public float RayDis = 1.5f;
    public float footOffset = 0.1f;
    public float Spd = 10f;

    private Vector3 CurrentPos, newPos, oldPos;
    private Vector3 OldPos, NewPos;
    private float Lerp;
    private Vector3 localOffset; // Distância relativa inicial
    private Quaternion localRotationOffset; // Rotação relativa inicial
    
    private Vector3 currentIkOffset; // O quanto o IK deslocou o pé verticalmente
    private float lerpY;
    private float targetIkY;
    private float oldIkY;

     void Start()
    {
        if (bodyTarget != null)
        {
            // Calcula a posição e rotação relativa no início
            localOffset = bodyTarget.InverseTransformPoint(transform.position);
            localRotationOffset = Quaternion.Inverse(bodyTarget.rotation) * transform.rotation;
        }
    }

    void Update()
    {
        if (bodyTarget == null) return;

        // 1. Seguir a posição e rotação do corpo (mantendo o offset original)
        Vector3 worldPos = bodyTarget.TransformPoint(localOffset);
        transform.rotation = bodyTarget.rotation * localRotationOffset;

        // 2. Lógica do IK apenas para a altura (eixo Y)
        ApplyHeightIK(worldPos);
    }

    void ApplyHeightIK(Vector3 basePosition)
    {
        // O raio agora sai da posição que o pé "deveria" estar se o chão fosse plano
        Ray ray = new Ray(basePosition + Vector3.up, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, RayDis, groundLayer))
        {
            float groundY = hit.point.y + footOffset;
            float differenceY = groundY - basePosition.y;

            // Se a diferença de altura mudar significativamente, inicia o Lerp
            if (Mathf.Abs(targetIkY - differenceY) > 0.05f)
            {
                lerpY = 0;
                oldIkY = currentIkOffset.y;
                targetIkY = differenceY;
            }
        }

        if (lerpY < 1)
        {
            lerpY += Time.deltaTime * Spd;
            currentIkOffset.y = Mathf.Lerp(oldIkY, targetIkY, lerpY);
        }

        // Aplica a posição base (que segue o corpo) + o ajuste de altura do IK
        transform.position = basePosition + Vector3.up * currentIkOffset.y;
    }
}
