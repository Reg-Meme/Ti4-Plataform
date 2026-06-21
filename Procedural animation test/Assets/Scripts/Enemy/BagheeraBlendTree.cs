using UnityEngine;
using UnityEngine.AI; 
public class BagheeraBlendTree : MonoBehaviour
{
    Animator ani;
    public NavMeshAgent agent; 
    
    int zParamHash;
    int xParamHash;

    void Start()
    {
        ani = GetComponent<Animator>();
        
        zParamHash = Animator.StringToHash("Z");
        xParamHash = Animator.StringToHash("X");
    }

    void Update()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
    
        float zVel = localVelocity.z;
        ani.SetFloat(zParamHash, zVel, 0.1f, Time.deltaTime);
        float xVel = localVelocity.x;
        ani.SetFloat(xParamHash, xVel, 0, Time.deltaTime);
    }
}
