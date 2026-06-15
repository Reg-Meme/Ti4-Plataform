using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewGame : MonoBehaviour
{
  

    public void StartLv1()
     {
        //StartCoroutine(SceneSwitch());

    //     SceneManager.LoadSceneAsync("Gameplay",LoadSceneMode.Additive);
         SceneManager.LoadScene("Ctscene1");
    //        SceneManager.UnloadSceneAsync("MainMenu");
   
    }
    IEnumerator SceneSwitch()
    {
        SceneManager.LoadSceneAsync("Gameplay", LoadSceneMode.Additive);
        AsyncOperation load = SceneManager.LoadSceneAsync("1Level", LoadSceneMode.Additive);
        GameManager.Instance.name = "1Level";  
        yield return load;                          
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
