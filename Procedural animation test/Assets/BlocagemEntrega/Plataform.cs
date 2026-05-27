using UnityEngine;

public class Plataform : MonoBehaviour
{
    public float tempoPraCair = 1f;
    public float tempoPraVoltar = 3f;

    Rigidbody rb;
    Collider col;

    Vector3 posInicial;

    bool ativada = false;
    float timer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        posInicial = transform.position;

        rb.isKinematic = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ativada = true;
        }
    }

    void Update()
    {
        if (ativada)
        {
            timer += Time.deltaTime;

            // Tremor
            transform.position = posInicial + Random.insideUnitSphere * 0.03f;

            // Cai
            if (timer >= tempoPraCair)
            {
                rb.isKinematic = false;
                col.enabled = false;

                ativada = false;

                Invoke(nameof(Voltar), tempoPraVoltar);
            }
        }
    }

    void Voltar()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = posInicial;

        rb.isKinematic = true;
        col.enabled = true;

        timer = 0;
    }
}