using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string name;
    bool isLoaded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
        // if(!isLoaded)
        //     {
        // SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        // SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        //         isLoaded = true;
        //     }
          SceneManager.LoadScene(name);  
        } 
    }
}
