using UnityEngine;
using UnityEditor;
using System;
[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position,Vector3.up,Vector3.forward,360,fov.fieldOfViewData.radius);

        Vector3 viewAngle1 = DirectionFromAngle(fov.transform.eulerAngles.y, - fov.fieldOfViewData.angle /2 );
        Vector3 viewAngle2 = DirectionFromAngle(fov.transform.eulerAngles.y,  fov.fieldOfViewData.angle /2 );

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle1 * fov.fieldOfViewData.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle2 * fov.fieldOfViewData.radius);
        
        if(fov.fieldOfViewData.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position,fov.fieldOfViewData.playerObj.transform.position );
        }
    }
    Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
