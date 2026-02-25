using UnityEngine;
using UnityEngine.Animations.Rigging;
public class IKKneeSolver : MonoBehaviour
{
    public Transform ikTarget; 
    public Transform Knee;
    Vector3 HintPos;
    public float MinHeightOffset = 0.1f;
    public Moviment BodyMov;
    MultiPositionConstraint MoveModes;
    public float ModeSwitchSpd = 5f;
    private float Weight = 0f; 
    void Start()
    {
    MoveModes = GetComponent<MultiPositionConstraint>();
    HintPos = transform.position;
    }
    void Update()
    {
    float ModeBlend = BodyMov.BottleMode ? 1f : 0f;
    Weight = Mathf.MoveTowards(Weight, ModeBlend, ModeSwitchSpd * Time.deltaTime);
    var modes = MoveModes.data.sourceObjects;
    modes.SetWeight(0, 1f - Weight); 
    modes.SetWeight(1, Weight);
    MoveModes.data.sourceObjects = modes;
    }
    public void FixedUpdate()
    {
        if (!BodyMov.BottleMode)
    {
        if(ikTarget.position.y < HintPos.y)
            {
            HintPos= Knee.position;
            }
    }
        
    }
    void LateUpdate()
    { 
        if (!BodyMov.BottleMode)
    {
            HintPos.y = Mathf.Max(HintPos.y, ikTarget.position.y + MinHeightOffset);
            transform.position = HintPos;
    }
    }
}
