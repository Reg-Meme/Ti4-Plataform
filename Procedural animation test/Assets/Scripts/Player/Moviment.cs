using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine.UI;
using UnityEditor.Rendering;


public class Moviment : MonoBehaviour
{
    public Transform Body;
    Rigidbody Rig;

    FixedJoint fixedJoint;
    CapsuleCollider BodyCollider;
    public IKFootSolverDaveJones[] Legs;
    PlayerInput Input;
    Gamepad Control;
    Inputs inputs;
    InputAction MoveAction;
    InputAction JumpAction;
    [SerializeField] InputActionReference Crouch;
    public LayerMask Ground;
    public float hoverRadius = 0.5f;
    public float HoverHeight = 2f;
    public float hoverForce = 100f;
    public float hoverDamp = 15f;
    public float coyoteDuration = 0.2f;
    float coyoteTimer;

    public float acceleration = 50f;
    public float maxSpeed = 15f;
    public float RollForce = 10f;
    public float movementSmoothTime = 0.01f;

    [Header("Jump")]

    public float jumpHeight = 8;
    public float lateralFriction = 5f;
    public float brakingDrag = 2f;
    public float stabilizer = 20f;
    public float Soften = 1.0f;

    public Vector2 currentInput;
    Vector2 inputVelocity;
    public float rotationSpeed = 10f;
    public bool BottleMode;
    public float HoverTim = 0.5f;
    float timer;
    public Vector3 BottleModeCOM;
    public float BMAngle;
    float CheckUpDis = 0.3f;
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
    public AnimationCurve moveForce;

    [Header("Gravity")]
    public float gravityForce = -9.81f;


    public ConstantForce gravity;
    bool hitGround;
    public Transform groundCheck;

    [Header("Camera Reference")]
    public Transform cameraTransform;

    bool NoBottleMode; //esse bool só serve pra não ficar tocando os 0 dos efeitos de rumble e Shake Toda hora
    void Awake()
    {
        inputs = new Inputs();
    }
    public void OnEnable()
    {
        inputs.Player.Enable();
    }
    public void OnDisable()
    {
        inputs.Player.Disable();
    }

    void Start()
    {
        cameraTransform = Camera.main.transform;
        fixedJoint = GetComponent<FixedJoint>();
        Rig = Body.GetComponent<Rigidbody>();
        BodyCollider = Body.GetComponent<CapsuleCollider>();
        Input = GetComponent<PlayerInput>();
        MoveAction = Input.actions.FindAction("Move");
        // JumpAction = Input.actions.FindAction("Jump");
        CamShake = CinCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        Control = Gamepad.current;
    }
    public void Update()
    {
        // Vector2 targetInput = MoveAction.ReadValue<Vector2>();
        Vector2 targetInput = inputs.Player.Move.ReadValue<Vector2>();

        currentInput = Vector2.SmoothDamp(currentInput, targetInput, ref inputVelocity, movementSmoothTime);
        float BodyAngle = Vector3.Angle(Body.up, Vector3.up);
        bool FacingDown = BodyAngle > BMAngle;

        if (!CellingChecker())
        {
            if (FacingDown)
            {
                BottleMode = true;
            }
            else
            {
                BottleMode = Crouch.action.IsPressed();
            }
        }
        //Camerashake
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
        Debug.Log("BottleMode: " + BottleMode);
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
            gravity.enabled = true;
            Rig.useGravity = false;
            if (timer <= 0)
            {
                Hover();
                jumpHeight=3;
            }
            Movement();
            Rotation();
            Friction();
            OnJump();
            JumpImprove();
            Stabilization();
        }
        else
        {
            jumpHeight=1.6f;
            Rig.linearDamping = 0.8f;
            Rig.angularDamping = 0;
            timer = HoverTim;
            gravity.enabled = false;
            Rig.useGravity = true;
            BottleMoviment();
            BodyCollider.height = 2.4f;
            Rig.centerOfMass = BottleModeCOM;
            Rig.mass = Mass;
        }
        if (isGrounded())
        {
            gravity.force = Physics.gravity;

        }
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

    void BottleMoviment()
    {
        Vector3 RollDir = new Vector3(currentInput.y, 0, -currentInput.x); // Eixo invertido para girar certo 
        Rig.AddTorque(RollDir * RollForce, ForceMode.Acceleration);
    }

    void Movement()
    {
        Transform cam = Camera.main.transform;

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * currentInput.y + right * currentInput.x;

        if (moveDir.magnitude > 1f) moveDir.Normalize();

        Vector3 horizontalVel = new Vector3(Rig.linearVelocity.x, 0, Rig.linearVelocity.z);
        Vector3 desiredVelocity = moveDir * maxSpeed;
        Vector3 velocityChange = desiredVelocity - horizontalVel;

        Rig.AddForce(velocityChange * acceleration, ForceMode.Acceleration);
    }
    void OnJump()
    {   
        if (inputs.Player.Jump.IsPressed() && isGrounded())
        {

           // Rig.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
           Rig.linearVelocity = Vector3.up * jumpHeight;

        }
        //Aumentar a gravidade caso o botão de pular seja solto
        if (!inputs.Player.Jump.IsPressed() && Rig.linearVelocity.y > 0)
        {
            Rig.AddForce(Vector3.down * Rig.linearVelocity.y * 0.3f, ForceMode.VelocityChange);
        }
    }
    void JumpImprove()
    {

        //Chegar na altura maxima do pulo
        if (Rig.linearVelocity.y < 0)
         //aumentar a gravidade da queda 
          SetGravityScale(1.5f, gravity);
    }
    void SetGravityScale(float gravityScale, ConstantForce gravity)
    {
        Vector3 gravityVec;
        gravityVec = new Vector3(0, gravityForce * gravityScale, 0);
        gravity.force = gravityVec;
    }
    public bool isGrounded()
    {
        bool cast = Physics.CheckSphere(groundCheck.position, 0.1f);
        if (cast)
        {
            Debug.DrawRay(groundCheck.position, Vector3.down, Color.purple);
            Debug.Log("chao");
            return true;
        }
        else
        {
            Debug.DrawRay(groundCheck.position, Vector3.down, Color.green);
            Debug.Log("nao estou no chao");
            return false;
        }
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
    
}