using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ActiveBatery : MonoBehaviour
{
    public GameObject[] batery;
    public int i = 0;
    public string Name;
    public Material ActivatedTxT;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //cutCall.SetActive(false);
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
            if(i >= 4) FazAlgo();
            Destroy(other.gameObject);
        }
    }
    [ContextMenu("Socorrororskoer")]
    void fazAlgo()
    {
        i++;
        Debug.Log("I ak" + i);
    }
    void FazAlgo()
    {
        //Debug.Log("TocaAnimacao");
        SceneManager.LoadScene(Name); 
    }
    public void ActivateMaterial()
    {
        GetComponent<Renderer>().material = ActivatedTxT;
    }
   
    
}
