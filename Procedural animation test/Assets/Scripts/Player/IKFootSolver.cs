using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEditor.Animations.Rigging;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
public class IKFootSolverDaveJones : MonoBehaviour
{
    public Rigidbody BodyRig;
    public Transform Body; 
    public LayerMask Ground;
    public IKFootSolverDaveJones OpLeg;

    public float DisStep;
    public float StepHeight;
    public float Spd;
    public Vector3 footOffset;
    public float PredictSpdMult;
    
    public float CheckerRadius;
    public float CheckerRadiusEdge;
    public float MaxStepDown;
    public float MaxStepUp;
    public bool IsGrounded;
    public float ModeSwitchSpd = 5f;
    private float Weight = 0f; 
    private Vector3 CurrentPos, OldPos, NewPos;
    private Vector3 LocalPos; 
    private float Lerp = 1;
    bool NearEdge;
    public bool IsMoving => Lerp < 1;
    public Moviment BodyMov;
    MultiPositionConstraint MoveModes;
    public float RayResizeSpd = 2f;
    private float RayResizeMult = 0f;
    public void Start()
    {
        MoveModes = GetComponent<MultiPositionConstraint>();
        LocalPos = transform.localPosition;
        CurrentPos = OldPos = NewPos = transform.position;
        
    }

    public void Update()
    {
        float ModeBlend = BodyMov.BottleMode ? 1f : 0f;
        Weight = Mathf.MoveTowards(Weight, ModeBlend, ModeSwitchSpd * Time.deltaTime);
        
        var modes = MoveModes.data.sourceObjects;
        modes.SetWeight(0, 1f - Weight); 
        modes.SetWeight(1, Weight);
        MoveModes.data.sourceObjects = modes;
        
            
        if (!BodyMov.BottleMode)
        {
            RayResizeMult = Mathf.MoveTowards(RayResizeMult, 1f, RayResizeSpd * Time.deltaTime);
                LegsNature(); 
            
        }
        else{
            RayResizeMult = 0f;
        }
    }

    public void LegsNature()
    {
        transform.position = CurrentPos;
        Vector3 footRestPos = Body.TransformPoint(LocalPos);
        Vector3 RayOg = footRestPos + Vector3.up * MaxStepUp;

        Vector3 Vel = BodyRig != null ? BodyRig.linearVelocity : Vector3.zero;
        Vel.y = 0;
        RayOg += Vel * PredictSpdMult;

        //quando sai do bottle mode, o tamanho do rai vai começar em 0 e dps ficar do tamanho normal
        // (isso é pra evita dele jogar a perna pra as cucuias e ss escolher a superfice fixável mais proxima)
        float TotalRayLen = (MaxStepDown + MaxStepUp) * RayResizeMult;
        if (TotalRayLen < 0.01f) 
        {
            IsGrounded = false;
            return;
        }

        NearEdge = !Physics.Raycast(RayOg, Vector3.down, TotalRayLen, Ground);
        float CurrentRadius = NearEdge ? CheckerRadiusEdge : CheckerRadius;

        RaycastHit hit;
        IsGrounded = Physics.SphereCast(RayOg, CurrentRadius, Vector3.down, out hit, TotalRayLen, Ground);
      
        if (IsGrounded)
        {
            if (hit.point.y > footRestPos.y + MaxStepUp)
            {
                IsGrounded = false;
            } 
        }

        if (IsGrounded)
        {
            if (Vector3.Distance(NewPos, hit.point) > DisStep && Lerp >= 1 && !OpLeg.IsMoving)
            {
                Lerp = 0;
                OldPos = CurrentPos;
                NewPos = hit.point + footOffset;
            }
        }

        if (Lerp < 1)
        {
            Vector3 tempPos = Vector3.Lerp(OldPos, NewPos, Lerp);
            tempPos.y += Mathf.Sin(Lerp * Mathf.PI) * StepHeight;
            CurrentPos = tempPos;
            Lerp += Time.deltaTime * Spd;
        }
        else
        {
            CurrentPos = NewPos;
        }
    }

    //Gizmos feitos no gepeto
    void OnDrawGizmos()
    {
        if (Body == null) return;

        Vector3 footRestPos = Body.TransformPoint(LocalPos);
        Vector3 rayOrigin = footRestPos + Vector3.up * MaxStepUp;
        
        if (Application.isPlaying && BodyRig != null)
        {
            Vector3 velocity = BodyRig.linearVelocity;
            velocity.y = 0;
            rayOrigin += velocity * PredictSpdMult;
        }

        float totalRayLen = MaxStepDown + MaxStepUp;
        float currentRadius = NearEdge ? CheckerRadiusEdge : CheckerRadius;

        // Visualização da Previsão (Azul)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rayOrigin, 0.05f);

        // Visualização do SphereCast (Verde se grounded, Vermelho se ar, Amarelo se expandido na quina)
        if (!IsGrounded) Gizmos.color = Color.red;
        else Gizmos.color = NearEdge ? Color.yellow : Color.green;

        Gizmos.DrawWireSphere(rayOrigin, currentRadius);
        Gizmos.DrawRay(rayOrigin, Vector3.down * totalRayLen);

        if (IsGrounded)
        {
            Gizmos.DrawSphere(NewPos, 0.1f);
            Gizmos.color = new Color(0, 1, 0, 0.1f);
            Gizmos.DrawWireSphere(NewPos, DisStep);
        }
    }
}