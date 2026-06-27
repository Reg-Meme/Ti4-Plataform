using UnityEngine;

public class DebugTools : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerStats.ClearCollectables();
            Debug.Log("Coletáveis limpos!");
        }
    }
}