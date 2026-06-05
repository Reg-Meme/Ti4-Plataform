using UnityEngine;

public class BangheeraTail : MonoBehaviour
{
    public enum Eixo { XY, XZ, YZ }

    [Header("Conexão")]
    public Transform BaseDaCauda; 

    [Header("Órbita Oval (O Rabo ao Redor)")]
    public Eixo EixoDaOrbita = Eixo.XZ;
    [Tooltip("Raio do primeiro eixo horizontal/vertical do plano escolhido")]
    public float RaioEixoA = 2.0f;
    [Tooltip("Raio do segundo eixo do plano escolhido. Valores diferentes de RaioEixoA tornam a órbita OVAL")]
    public float RaioEixoB = 1.0f;
    public float VelocidadeOrbita = 3f;

    [Header("Modificadores Orgânicos (Efeito Blade Wolf)")]
    [Tooltip("O quanto o formato oval deforma dinamicamente ao longo do tempo")]
    public float PulsacaoRaio = 0.3f;
    [Tooltip("Velocidade da variação do tamanho e velocidade")]
    public float FrequenciaVariacao = 2.5f;
    [Tooltip("Micro-tremores rápidos nas pontas (aspecto mecânico)")]
    public float IntensidadeTremor = 0.15f;
    [Tooltip("Suavidade do atraso (valores menores dão mais efeito de chicote)")]
    public float SuavidadeSeguir = 10f;

    private float _angulo;
    private float _sementeRuido;

    void Start()
    {
        _sementeRuido = Random.Range(0f, 100f);
        if (BaseDaCauda == null && transform.parent != null)
        {
            BaseDaCauda = transform.parent;
        }
    }

    void Update()
    {
        if (BaseDaCauda == null) return;

        // 1. Velocidade dinâmica: acelera e desacelera para quebrar o ritmo mecânico
        float variacaoVelocidade = Mathf.Sin(Time.time * FrequenciaVariacao) * 0.4f + 1f; 
        _angulo += VelocidadeOrbita * variacaoVelocidade * Time.deltaTime;

        // 2. Modificador orgânico que faz o tamanho pulsar de leve para não ser um oval estático
        float pulsoOrganico = Mathf.Cos(_angulo * 1.5f) * PulsacaoRaio;
        float raioDinamicoA = RaioEixoA + pulsoOrganico;
        float raioDinamicoB = RaioEixoB + pulsoOrganico;

        // 3. Calcula a órbita matemática oval
        Vector3 offsetOrbita = CalcularOffsetOval(_angulo, raioDinamicoA, raioDinamicoB, EixoDaOrbita);

        // 4. Adiciona o ruído Perlin para os micro-tremores robóticos
        float tremorX = (Mathf.PerlinNoise(Time.time * 12f, _sementeRuido) - 0.5f) * IntensidadeTremor;
        float tremorY = (Mathf.PerlinNoise(_sementeRuido, Time.time * 12f) - 0.5f) * IntensidadeTremor;
        float tremorZ = (Mathf.PerlinNoise(Time.time * 10f, _sementeRuido * 2f) - 0.5f) * IntensidadeTremor;
        Vector3 offsetTremor = new Vector3(tremorX, tremorY, tremorZ);

        // 5. Aplica a rotação da base para o rabo acompanhar a direção do personagem
        Vector3 posicaoDesejada = BaseDaCauda.position + (BaseDaCauda.rotation * (offsetOrbita + offsetTremor));

        // 6. Cria o efeito chicote (atraso físico por interpolação)
        transform.position = Vector3.Lerp(transform.position, posicaoDesejada, Time.deltaTime * SuavidadeSeguir);

        // 7. Rotação: Aponta na direção do movimento orbital
        Vector3 direcaoMovimento = transform.position - BaseDaCauda.position;
        if (direcaoMovimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direcaoMovimento), Time.deltaTime * SuavidadeSeguir);
        }
    }

    Vector3 CalcularOffsetOval(float angulo, float raioA, float raioB, Eixo eixo)
    {
        // Ao usar raioA para o Cosseno e raioB para o Seno, criamos a elipse/oval
        float cos = Mathf.Cos(angulo) * raioA;
        float sin = Mathf.Sin(angulo) * raioB;

        switch (eixo)
        {
            // EixoA controla o primeiro termo, EixoB controla o segundo termo do plano
            case Eixo.XY: return new Vector3(cos, sin, 0);
            case Eixo.XZ: return new Vector3(cos, 0, sin); // Ideal para orbitar ao redor da cintura
            case Eixo.YZ: return new Vector3(0, cos, sin);
            default: return Vector3.zero;
        }
    }
}
