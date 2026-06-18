using UnityEngine;

public class EditorCameraRuntime : MonoBehaviour
{
    [Header("Configurações de Velocidade")]
    public float baseMoveSpeed = 10f;
    public float fastMoveSpeed = 30f;
    public float panSpeed = 20f;

    [Header("Configurações de Rotação")]
    public float lookSpeed = 2f;

    private float pitch = 0f;
    private float yaw = 0f;

    void Start()
    {
        // Salva a rotação inicial para a câmera não dar um "pulo" no primeiro clique
        Vector3 rot = transform.eulerAngles;
        pitch = rot.x;
        yaw = rot.y;
    }

    void Update()
    {
        // 1. ROTAÇÃO E MOVIMENTO (Botão Direito do Mouse)
        // Na Unity, você geralmente voa pelo mapa enquanto segura o botão direito
        if (Input.GetMouseButton(1))
        {
            // --- Olhar em volta ---
            yaw += Input.GetAxis("Mouse X") * lookSpeed;
            pitch -= Input.GetAxis("Mouse Y") * lookSpeed;
            
            // Trava o eixo Y para não dar cambalhotas (limite de 90 graus)
            pitch = Mathf.Clamp(pitch, -90f, 90f);

            transform.eulerAngles = new Vector3(pitch, yaw, 0f);

            // --- Voar com WASD e QE ---
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? fastMoveSpeed : baseMoveSpeed;

            float moveX = Input.GetAxis("Horizontal"); // A e D
            float moveZ = Input.GetAxis("Vertical");   // W e S
            float moveY = 0f;

            // Q desce, E sobe
            if (Input.GetKey(KeyCode.E)) moveY = 1f;
            if (Input.GetKey(KeyCode.Q)) moveY = -1f;

            // Calcula a direção baseada para onde a câmera está olhando
            Vector3 move = (transform.right * moveX) + (transform.up * moveY) + (transform.forward * moveZ);
            transform.position += move * currentSpeed * Time.deltaTime;
        }

        // 2. PANNING / ARRASTAR (Botão do Meio / Scroll Wheel)
        if (Input.GetMouseButton(2))
        {
            // Lemos o movimento do mouse e invertemos (negativo) para dar a sensação de "puxar" a tela
            float panX = -Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
            float panY = -Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;

            transform.position += (transform.right * panX) + (transform.up * panY);
        }

        // 3. ZOOM (Rodar o Scroll do Mouse)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            // Move a câmera para frente ou para trás baseada na direção em que está olhando
            transform.position += transform.forward * scroll * baseMoveSpeed;
        }
    }
}