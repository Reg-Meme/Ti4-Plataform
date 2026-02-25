using UnityEngine;

public class TrackerPosition : MonoBehaviour
{
   private GameObject tracker;
    private Material grassMat;

    void Start()
    {
        grassMat = GetComponent<Renderer>().material;
        tracker = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector3 trackerPos = tracker.GetComponent<Transform>().position;

        grassMat.SetVector("_TrackerPos", trackerPos);
    }
}
