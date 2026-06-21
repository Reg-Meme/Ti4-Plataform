using System;
using UnityEngine;

public class CutsceneBarManager : MonoBehaviour
{

    public float Clip;
   

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalFloat("_MapBGClipping", Clip); 
    }
    
    
    
}
