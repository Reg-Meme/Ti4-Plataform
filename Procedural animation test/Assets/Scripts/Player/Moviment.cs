using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine.UI;
using UnityEditor.Rendering;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Scripting;




public class Moviment : MonoBehaviour
{
    public Transform Body;
    Rigidbody Rig;

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
    [Header("Speed")]
    public float acceleration = 50f;
    public float maxSpeed = 15f;
    public float RollForce = 10f;
    public float movementSmoothTime = 0.01f;
    public float MaxRotSpd = 20;
    [Header("Jump")]

    public float jumpHeight = 8;
    public float lateralFriction = 5f;
    public float brakingDrag = 2f;
    public float stabilizer = 20f;
    public float Soften = 1.0f;

    public Vector2 currentInput;
    public Vector2 inputValue;

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
    public float gravityForce = -9.81f;


    public ConstantForce gravity;
    bool hitGround;
    public Transform groundCheck;
    [Header("WallWalk")]
    public bool jumping;
    public Vector3 myNormal;
    public float distGround;
    public Vector3 surfaceNormal;
    public LayerMask wallLayer;

    //public List<Move> move = new List<Move>();
    public Move[] move = new Move[2];

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

    }


    void Start()
    {
        fixedJoint = GetComponent<FixedJoint>();
        Rig = Body.GetComponent<Rigidbody>();
        //Rig.freezeRotation = true;
        BodyCollider = Body.GetComponent<CapsuleCollider>();
        CamShake = CinCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        Control = Gamepad.current; 
        move[0] = new Walk();
        move[1] = new Roll(Body, Ground, transform);
         distGround = BodyCollider.bounds.extents.y - BodyCollider.center.y; 
         myNormal = transform.up; 
       


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
                jumpHeight = 5;
            }


            move[0].Movimentation(currentInput, Rig, maxSpeed);
            Rotation();
            Friction();

            JumpImprove();
            Stabilization();
        }
        else
        {
            jumpHeight = 1.6f;
            Rig.linearDamping = 0.8f;
            Rig.angularDamping = 0;
            timer = HoverTim;
            gravity.enabled = false;
            Rig.useGravity = true;

            move[1].Movimentation(currentInput, Rig, MaxRotSpd);
            //BottleMoviment();
            BodyCollider.height = 2.4f;

            Rig.mass = Mass;
        }
        
        if (!isGrounded())
        {
            //Rig.AddForce(gravityForce* Rig.mass * myNormal);
            gravity.force = Physics.gravity;

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

        Vector3 vel = Rig.linearVelocity;
        Vector3 desiredVelocity = moveDir * maxSpeed;
        //Vector3 velocityChange = desiredVelocity - horizontalVel;
        vel.x = desiredVelocity.x;
        vel.z = desiredVelocity.z;
        Rig.linearVelocity = vel;
        //Rig.AddForce(velocityChange * acceleration, ForceMode.Acceleration);
    }
    void OnJump()
    {
        // Rig.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
        
        RaycastHit hit;
      
        if(Physics.Raycast(transform.position, transform.forward, out hit, 5,wallLayer))
        {
            Debug.Log("AAAAAAAAAAAAA");
           JumpToWall(hit.point, hit.normal);
        }

        if (isGrounded())
            Rig.linearVelocity = Vector3.up * jumpHeight;

    }
    void OnJumpRelease()
    {
        //Aumentar a gravidade caso o botão de pular seja solto
        if (Rig.linearVelocity.y > 0 && !isGrounded())
        {
            Rig.AddForce(Vector3.down * Rig.linearVelocity.y * 0.3f, ForceMode.VelocityChange);

        }
    }
     void JumpToWall( Vector3 point, Vector3 normal ){
    // jump to wall 
    jumping = true; // signal it's jumping to wall
    Rig.isKinematic = true; // disable physics while jumping
    var orgPos = transform.position;
    var orgRot = transform.rotation;
    Vector3 dstPos = point + normal * (distGround + 0.5f); // will jump to 0.5 above wall
    var myForward = Vector3.Cross(transform.right, normal);
    var dstRot = Quaternion.LookRotation(myForward, normal);
    for (float t = 0; t < 1.0; ){
        t += Time.deltaTime;
        transform.position = Vector3.Lerp(orgPos, dstPos, t);
        transform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
        return; // return here next frame
    }
    myNormal = normal; // update myNormal
    Rig.isKinematic = false; // enable physics
    jumping = false; // jumping to wall finished
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