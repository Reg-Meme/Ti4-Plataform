using UnityEngine;

public class CollectableSceneSetup : MonoBehaviour
{
    private void Start()
    {
        Collectable[] all = FindObjectsByType<Collectable>( FindObjectsInactive.Include,FindObjectsSortMode.None);
        if (all.Length > PlayerStats.totalCollectables)PlayerStats.SaveTotalCollectables(all.Length);
    }
}