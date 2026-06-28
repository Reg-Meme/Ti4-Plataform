using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
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
        //         StartCoroutine(NewScene());
        //         isLoaded = true;
        //     }
          PlayerStats.lastScene = name;
          PlayerStats.haveCheckPoint = false;
          PlayerStats.musicTime = 0;
          PlayerStats.clip = null;
          PlayerStats.SaveStats();

          SceneManager.LoadScene(name);  
        } 
    }
    IEnumerator NewScene()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        GameManager.Instance.name = name;
        yield return load;
        
    }
}
