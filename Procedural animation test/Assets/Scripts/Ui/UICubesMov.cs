using UnityEngine;

public class UICubesMov : MonoBehaviour
{
     [Header("Círculo Principal")]
    public float raio = 5f;
    public float velocidadeOrbita = 2f;
    public enum Eixo { XY, XZ, YZ }
    public Eixo planoPrincipal = Eixo.XZ;

    [Header("Movimentação Extra (Círculo Secundário)")]
    public float raioExtra = 1f;
    public float velocidadeExtra = 5f;
    public Eixo planoExtra = Eixo.XY;

    [Header("Configurações de Rotação (Y)")]
    public float velocidadeRotacaoY = 100f;
    public bool sentidoHorario = true;

    private float anguloPrincipal;
    private float anguloExtra;
    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
        // 1. Cálculo do Círculo Principal
        anguloPrincipal += velocidadeOrbita * Time.deltaTime;
        Vector3 offsetPrincipal = CalcularOffset(anguloPrincipal, raio, planoPrincipal);

        // 2. Cálculo da Movimentação Extra (Segundo Círculo)
        anguloExtra += velocidadeExtra * Time.deltaTime;
        Vector3 offsetExtra = CalcularOffset(anguloExtra, raioExtra, planoExtra);

        // Somamos a posição inicial + os dois círculos
        transform.position = posicaoInicial + offsetPrincipal + offsetExtra;

        // 3. Rotação em Y
        float direcao = sentidoHorario ? 1f : -1f;
        transform.Rotate(Vector3.up, velocidadeRotacaoY * direcao * Time.deltaTime);
    }

    // Função auxiliar para não repetir código de switch
    Vector3 CalcularOffset(float tempo, float r, Eixo eixo)
    {
        float cos = Mathf.Cos(tempo) * r;
        float sin = Mathf.Sin(tempo) * r;

        switch (eixo)
        {
            case Eixo.XY: return new Vector3(cos, sin, 0);
            case Eixo.XZ: return new Vector3(cos, 0, sin);
            case Eixo.YZ: return new Vector3(0, cos, sin);
            default: return Vector3.zero;
        }
    }
}
