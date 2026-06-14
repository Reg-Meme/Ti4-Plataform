using UnityEngine;
using UnityEngine.SceneManagement;
public class NewGame : MonoBehaviour
{

    public void StartLv1()
    {
        SceneManager.LoadSceneAsync("Gameplay",LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("1Level",LoadSceneMode.Additive);
    }
}
