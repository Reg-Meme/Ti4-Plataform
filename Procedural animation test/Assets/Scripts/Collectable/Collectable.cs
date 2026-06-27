using UnityEngine;

public class Collectable : MonoBehaviour
{
    [Tooltip("Nome único deste coletável")]
    public string collectableName;

    private void Start()
    {
        if (PlayerStats.collectedItems.Contains(collectableName)) gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerStats.AddCollectable(collectableName);
        CollectableUI.Instance.ShowCollectable(collectableName);
        gameObject.SetActive(false);
    }
}