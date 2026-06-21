using UnityEngine;

public class UnscaledTime : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalFloat("_time",Time.unscaledTime);
    }
}
