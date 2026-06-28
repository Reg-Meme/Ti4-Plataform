using UnityEngine;
using TMPro;

public class CollectablePanel : MonoBehaviour
{
    [Header("UI")]
    public Transform listContainer;
    public GameObject itemPrefab;
    public TextMeshProUGUI summaryText;

    private void OnEnable()
    {
        PopulateList();
    }

    private void PopulateList()
    {
        foreach (Transform child in listContainer)Destroy(child.gameObject);

        PlayerData data = PlayerStats.LoadStats();
        if (data == null)
        {
            summaryText.text = "Nenhum dado salvo.";
            return;
        }

        int collected = data.collectedItems.Count;
        int total = data.totalCollectables;

        summaryText.text = $"{collected}/{total} coletados";

        foreach (string itemName in data.collectedItems)
        {
            GameObject item = Instantiate(itemPrefab, listContainer);
            item.GetComponentInChildren<TextMeshProUGUI>().text = itemName;
        }
    }
}