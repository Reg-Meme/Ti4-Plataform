using UnityEngine;

public class MapBGSpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalFloat("_time",Time.unscaledTime);
    }
}
