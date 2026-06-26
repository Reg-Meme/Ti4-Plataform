using Unity.VisualScripting;
using UnityEngine;

public class ActiveBatery : MonoBehaviour
{
    public GameObject[] batery;
    public int i = 0;

    public Material ActivatedTxT;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < batery.Length; i++)
        {
            batery[i].SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Batery"))
        {
            batery[i].SetActive(true);
            i++;
            Destroy(other.gameObject);
        }
    }

    public void ActivateMaterial()
    {
        GetComponent<Renderer>().material = ActivatedTxT;
    }
   
    
}
