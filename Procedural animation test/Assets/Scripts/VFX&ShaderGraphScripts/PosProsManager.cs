using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
public class PosProsManager : MonoBehaviour
{
   private Volume volume;
    private ColorAdjustments colorAdjustments;
    private ShadowsMidtonesHighlights GameColor;

    void Awake()
    {

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        volume = GetComponent<Volume>();
        if (volume != null && volume.profile != null)
        {
            volume.profile.TryGet(out colorAdjustments);
            volume.profile.TryGet(out GameColor);
        }
    }

    void Update()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (colorAdjustments != null && GameColor != null)
        {
            if (currentSceneIndex == 0)
            {
                colorAdjustments.active = true;
                GameColor.active = false;
            }
            else
            {
                colorAdjustments.active = false;
                GameColor.active = true;
            }
        }
    }
}
