using System;
using UnityEngine;

public class CutsceneBarManager : MonoBehaviour
{

    public float Clip;
    void Start()
    {
      //  Shader.SetGlobalFloat("_MapBGClipping", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalFloat("_MapBGClipping", Clip); 
    }
    
    
    
}
