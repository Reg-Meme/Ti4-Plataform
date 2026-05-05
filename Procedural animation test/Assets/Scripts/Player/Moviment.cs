using UnityEngine;

using UnityEngine.InputSystem;

using Unity.Cinemachine;

using UnityEngine.SceneManagement;
using System.Collections;
using NUnit.Framework;


public class Moviment : MonoBehaviour
{
    public Transform Body;
    Rigidbody Rig;
    public static Moviment moviment;

    FixedJoint fixedJoint;
    CapsuleCollider BodyCollider;
    public IKFootSolverDaveJones[] Legs;

    Gamepad Control;
    [SerializeField] private InputInfo inputInfo;

    [SerializeField] InputActionReference Crouch;
    public LayerMask Ground;
    [Header("Hover")]
    public float hoverRadius = 0.5f;
    public float HoverHeight = 2f;
    public float hoverForce = 100f;
    public float hoverDamp = 15f;
    public float coyoteDuration = 0.2f;
    float coyoteTimer;

    public float jumpCoyoteDuration = 0.2f;
    public float jumpTimer;


    public float inputBuffer = 0.2f;
    public float inputTimer;
    [Header("Speed")]
    public float acceleration = 50f;
    public float maxSpeed = 15f;
    public float RollForce = 10f;
    public float movementSmoothTime = 0.01f;
    public float MaxRotSpd = 20;
    [Header("Jump")]

    public float jumpHeight = 8;
    public float inicialJumpHeight;
    public float lateralFriction = 5f;
    public float brakingDrag = 2f;
    public float stabilizer = 20f;
    public float Soften = 1.0f;

    public Vector2 currentInput;
    public Vector2 inputValue;
    public bool canJump = true;
    public bool pressJump = false;
    Vector2 inputVelocity;
    public float rotationSpeed = 10f;
    public bool BottleMode;
    bool FacingDown;
    public float HoverTim = 0.5f;
    float timer;
    public Vector3 BottleModeCOM;
    public float BMAngle;

    float CheckUpDis = 0.3f;
    public float RollCheck;
    public CinemachineCamera CinCam;
    CinemachineBasicMultiChannelPerlin CamShake;
    public float DecMag;
    public float CamShakeAmp;
    public float CamShakeFreq;
    public float DecMagLil;
    public float DecMagASF;
    public float CamShakeAmpASF;
    public float CamShakeFreqASF;
    public float Mass;


    [Header("Gravity")]

    public bool hitGround;
    public Transform groundCheck;
    public float radius;
    public float assradius;
    [SerializeField] PhysicsMaterial physicsMaterial;
    public bool[] casts = new bool[4];
    public Transform[] legsPosition = new Transform[4];
    public int count = 0;


    //public List<Move> move = new List<Move>();
    public Move[] move = new Move[2];
    [Header("Blob Shadow")]
    public GameObject DownShadowObj;
    public float ShadowOffset;

    bool NoBottleMode; //esse bool só serve pra não ficar tocando os 0 dos efeitos de rumble e Shake Toda hora
    void Awake()
    {
        inputInfo.Initialize();

        //atribuicao de eventos
        InputInfo.OnMoveEvent += MoveInput;
        InputInfo.OnJumpEvent += OnJump;
        InputInfo.OnReleaseJumpEvent += OnJumpRelease;
        InputInfo.OnResetEvent += ResetLevel;
        InputInfo.OnCrouchEvent += BottleModeEnter;
        InputInfo.OnCrouchReleaseEvent += BottleModeExit;
        if (moviment == null) moviment = this;
    }
    
    public Roll roll;

    void Start()
    {
        fixedJoint = GetComponent<FixedJoint>();
        Rig = Body.GetComponent<Rigidbody>();

        BodyCollider = Body.GetComponent<CapsuleCollider>();
        CamShake = CinCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        Control = Gamepad.current;
        move[0] = new Walk();
        move[1] = new Roll(Body, Ground, transform);
        inicialJumpHeight = jumpHeight;
        

    }
    public void Update()
    {


        currentInput = Vector2.SmoothDamp(currentInput, inputValue, ref inputVelocity, movementSmoothTime);
        float BodyAngle = Vector3.Angle(Body.up, Vector3.up);
        FacingDown = BodyAngle > BMAngle;

        if (BottleMode)
        {
            bool isMovingLil = Rig.linearVelocity.magnitude > DecMagLil;
            bool isMoving = Rig.linearVelocity.magnitude > DecMag;
            bool isMovingASF = Rig.linearVelocity.magnitude > DecMagASF;
            if (OnOffOptions.Instance.CameraShake)
            {
                CamShake.AmplitudeGain = isMovingASF ? CamShakeAmpASF : (isMoving ? CamShakeAmp : 0f);
                CamShake.FrequencyGain = isMovingASF ? CamShakeFreqASF : (isMoving ? CamShakeFreq : 0f);
            }

            if (Control != null)
            {
                if (OnOffOptions.Instance.Rumble)
                {
                    if (isMovingASF)
                        Control.SetMotorSpeeds(0.21f, 0.3f);
                    else if (isMoving)
                        Control.SetMotorSpeeds(0.1f, 0.15f);
                    else if (isMovingLil)
                        Control.SetMotorSpeeds(0.01f, 0.08f);
                    else
                        Control.SetMotorSpeeds(0f, 0f);
                }
            }
        }
        else if (NoBottleMode)
        {
            CamShake.AmplitudeGain = 0f;
            CamShake.FrequencyGain = 0f;
            Control?.SetMotorSpeeds(0f, 0f);
        }
        NoBottleMode = BottleMode;
    }
    

    public void FixedUpdate()
    {
        if (isGrounded())
            Debug.Log("Estouj no chao");
        else Debug.Log("nao estou no chao");


        if (!hitGround)
        {
            //Debug.Log("to fazendo algo aqui ");
        }
        CellingChecker();
        if (!BottleMode)
        {
            Rig.centerOfMass = Vector3.zero;
            Rig.linearDamping = 0.5f;
            Rig.angularDamping = 2;
            BodyCollider.height = 2f;
            BodyCollider.center = Vector3.zero;
            //Rig.centerOfMass = new Vector3(0,-1,0);
            Rig.mass = 2;
            //fixedJoint.connectedMassScale =3;
            timer -= Time.fixedDeltaTime;

            if (timer <= 0)
            {
                Hover();
                //jumpHeight = 5;
            }


            move[0].Movimentation(currentInput, Rig, maxSpeed);
            radius = 0.2f;
            Rotation();
            Friction();
            Coyote();
            JumpBuffer();
            Atrito();
            //JumpImprove();
            Stabilization();
            DownShadow();
            DownShadowObj.SetActive(true);
        }
        else
        {
            //jumpHeight = 1.6f;
            Rig.linearDamping = 0.8f;
            Rig.angularDamping = 0;
            timer = HoverTim;
            move[1].Movimentation(currentInput, Rig, MaxRotSpd);
            //BottleMoviment();
            BodyCollider.height = 2.4f;
            radius = 0.6f;
            Rig.mass = Mass;
            DownShadowObj.SetActive(false);
        }


    }


    void BottleModeEnter()
    {

        if (!CellingChecker())
        {
            if (!FacingDown)
                BottleMode = true;

        }
    }
    void BottleModeExit()
    {
        BottleMode = false;
        
    }
    void ResetLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool CellingChecker()
    {
        bool Ray = Physics.Raycast(Body.position, Vector3.up, CheckUpDis, Ground);
        if (Ray)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Rotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    private void MoveInput(Vector2 v2)
    {
        inputValue = v2;
    }
    
    void OnJump()
    {
        if (BottleMode) return;
        inputTimer = inputBuffer;
        // Rig.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
        if (canJump)
        {


            Rig.linearVelocity = Vector3.up * jumpHeight;
        }

    }
    void OnJumpRelease()
    {
        //Aumentar a gravidade caso o botão de pular seja solto
        if (Rig.linearVelocity.y > 0 && !isGrounded())
        {
            canJump = false;
            Rig.AddForce(Vector3.down * Rig.linearVelocity.y * 0.3f, ForceMode.VelocityChange);

        }
    }
    void Atrito()
    {
        float atrito = isGrounded() ? 0.6f : 0;

        Debug.Log("atrito" + atrito);
        if (Rig.linearVelocity.magnitude > 0)
        {
            physicsMaterial.dynamicFriction = atrito;
            physicsMaterial.staticFriction = atrito;
        }

    }
    void Coyote()
    {

        if (isGrounded()) canJump = true;
        else StartCoroutine(CanJump());

    }
    IEnumerator CanJump()
    {
        yield return new WaitForSeconds(jumpTimer);
        canJump = false;
    }
    void JumpBuffer()
    {
        inputTimer -= Time.deltaTime;
        if (inputTimer > 0 && isGrounded())
        {
            OnJump();
            inputTimer = 0;
        }
    }

    // void JumpImprove()
    // {

    //     //Chegar na altura maxima do pulo
    //     if (Rig.linearVelocity.y < 0)
    //         //aumentar a gravidade da queda 
    //         SetGravityScale(1.5f, gravity);
    // }
    // void SetGravityScale(float gravityScale, ConstantForce gravity)
    // {
    //     Vector3 gravityVec;
    //     gravityVec = new Vector3(0, gravityForce * gravityScale, 0);
    //     gravity.force = gravityVec;
    // }
    public bool isGrounded()
    {
        
        
        // bool cast1 = Physics.CheckSphere(Legs[0].transform.position, radius, Ground);
        // bool cast2 = Physics.CheckSphere(groundCheck.position, radius, Ground);l
        // bool cast3 = Physics.CheckSphere(groundCheck.position, radius, Ground);
        // bool cast4 = Physics.CheckSphere(groundCheck.position, radius, Ground);
        // casts[0] = cast1;
        // casts[1] = cast2;
        // casts[2] = cast3;
        // casts[3] = cast4;
        for (int i = 0; i < casts.Length; i++)
        {
            casts[i] = Physics.Raycast(legsPosition[i].transform.position, Vector3.down, radius, Ground);
        }
       

        if (casts[2] && casts[3] || casts[0] && casts[1] && casts[2] && casts[3])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isAssGrounded()
    {
       bool cast = Physics.Raycast(groundCheck.transform.position, Vector3.down, assradius, Ground);
       if (cast)
        {
        return true;
        }
        else
        {
           return false;
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        // 1. Proteção: Evita erros no console caso você esqueça de arrastar o Transform no Inspector
        if (groundCheck == null) return;

        // 2. Dica Pro: Mudar a cor do Gizmo dependendo se está tocando o chão ou não!
        if (isGrounded())
        {
            Gizmos.color = Color.purple; // Tocando o chão = Verde
        }
        else
        {
            Gizmos.color = Color.red;   // No ar = Vermelho
        }

        // 3. Desenha uma esfera "aramada" exatamente na mesma posição e raio do CheckSphere
        for (int i = 0; i < casts.Length; i++)
        {

            Gizmos.DrawRay(legsPosition[i].transform.position, Vector3.down * radius);
        }
        // Se preferir uma esfera sólida e semitransparente, use:
        // Gizmos.DrawSphere(groundCheck.position, radius);
    }




    void Friction()
    {
        Vector3 rightDir = Body.right;

        float sidewaysVel = Vector3.Dot(Rig.linearVelocity, rightDir);

        Vector3 frictionForce = -rightDir * sidewaysVel * lateralFriction;

        Rig.AddForce(frictionForce, ForceMode.Acceleration);
    }

    void Hover()
    {
        RaycastHit hit;
        hitGround = Physics.SphereCast(Body.position, hoverRadius, Vector3.down, out hit, HoverHeight, Ground);


        if (hitGround)
        {
            coyoteTimer = coyoteDuration;
        }
        else
        {
            coyoteTimer -= Time.fixedDeltaTime;
        }

        if (coyoteTimer > 0)
        {
            float currentDistance = hitGround ? hit.distance : HoverHeight;
            float heightOff = HoverHeight - currentDistance;
            float upwardVel = Vector3.Dot(Rig.linearVelocity, Vector3.up);
            float dampingForce = upwardVel * hoverDamp;

            if (upwardVel < 0 && heightOff < (HoverHeight * 0.5f))
            {
                dampingForce = 0;
            }

            float coyoteFade = hitGround ? 1.0f : (coyoteTimer / coyoteDuration);
            Vector3 antiGravity = hitGround ? Vector3.zero : -Physics.gravity;

            if (heightOff > 0 || !hitGround)
            {
                float springForce = heightOff * hoverForce;
                Vector3 totalForce = Vector3.up * (springForce - dampingForce + (antiGravity.y * Rig.mass));
                Rig.AddForce(totalForce * coyoteFade, ForceMode.Acceleration);
            }
        }
    }

    void Stabilization()
    {
        Quaternion stabilize = Quaternion.FromToRotation(Body.up, Vector3.up);
        Vector3 torque = new Vector3(stabilize.x, 0, stabilize.z) * stabilizer;
        Rig.AddTorque(torque - Rig.angularVelocity * Soften);
    }


    public void DownShadow()
    {
        RaycastHit hit;

        if (Physics.Raycast(groundCheck.transform.position, Vector3.down, out hit, 50f))
        {
            DownShadowObj.SetActive(true);
            DownShadowObj.transform.position = hit.point + hit.normal * ShadowOffset;
            DownShadowObj.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * Quaternion.Euler(90, 0, 0);
        }
        else
        {
            DownShadowObj.SetActive(false);
        }
    }


}