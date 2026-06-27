using System.Collections;
using TMPro;
using UnityEngine;

public class CollectableUI : MonoBehaviour
{
    public static CollectableUI Instance;

    [Header("UI")]
    public TextMeshProUGUI collectableText;

    [Header("Timing")]
    public float fadeInDuration = 0.5f;
    public float stayDuration = 2f;
    public float fadeOutDuration = 0.8f;

    private Coroutine currentCoroutine;
    private int totalInScene;

    private void Awake()
    {
        Instance = this;
        SetAlpha(0f);
    }

    private void Start()
    {
        totalInScene = FindObjectsByType<Collectable>(FindObjectsInactive.Include,FindObjectsSortMode.None).Length;
    }

    public void ShowCollectable(string itemName)
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);

        int collected = PlayerStats.collectedItems.Count;
        collectableText.text = $"{itemName}  {collected}/{totalInScene}";

        currentCoroutine = StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        yield return StartCoroutine(Fade(0f, 1f, fadeInDuration));
        yield return new WaitForSeconds(stayDuration);
        yield return StartCoroutine(Fade(1f, 0f, fadeOutDuration));
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            SetAlpha(Mathf.Lerp(from, to, elapsed / duration));
            yield return null;
        }
        SetAlpha(to);
    }

    private void SetAlpha(float alpha)
    {
        Color c = collectableText.color;
        c.a = alpha;
        collectableText.color = c;
    }
}