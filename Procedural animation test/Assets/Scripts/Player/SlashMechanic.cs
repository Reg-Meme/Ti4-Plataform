using UnityEngine;
using EzySlice;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class SlashMechanic : Mechanics
{

    public GameObject cutPlane;
    public GameObject slashCam;
    public GameObject aimSlashCam;
    public Material crossSectionMat;
    public float cutPlanSpeed = 5;
    public bool PlayerStatsbladeMode;
    public LayerMask layerMask;
    //camera shake
    public float amp = 1;
    public float freq = 2;
    public float dur = 3;
    
    

    public SlashMechanic(GameObject cutPlane, GameObject slashCam, GameObject aimSlashCam, Material crossSectionMat, LayerMask layerMask)
    {
        this.cutPlane = cutPlane;
        this.slashCam = slashCam;
        this.aimSlashCam = aimSlashCam;
        this.crossSectionMat = crossSectionMat;
        this.layerMask = layerMask;


    }

    public override void AttackButton()
    {
       
        if (PlayerStats.bladeMode)
        {
        if (!battery.Consume(batteryCost))return;
          

            Collider[] hits = Physics.OverlapBox(cutPlane.transform.position, new Vector3(1, 0.1f, 1), cutPlane.transform.rotation, layerMask);
            if (hits.Length <= 0) return;
            for (int i = 0; i < hits.Length; i++)
            {
             
                SlicedHull hull = SliceObject(hits[i].gameObject, crossSectionMat);
                if (hull != null)
                {
                    
                    GameObject bottom = hull.CreateLowerHull(hits[i].gameObject, crossSectionMat);
                    GameObject top = hull.CreateUpperHull(hits[i].gameObject, crossSectionMat);
                    AddHullComponents(top);
                    AddHullComponents(bottom);
                    Object.Destroy(hits[i].gameObject);
                }
            }
        }
    }
    public void AddHullComponents(GameObject go)
    {
        CameraShakeManager.Shaker.ShakePulse(amp, freq, dur);
        go.layer = LayerMask.NameToLayer("Cut");
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;

        rb.AddExplosionForce(100, go.transform.position, 10);
        Object.Destroy(go, 5f);
    }
    public void RotatePlan(Vector2 AxisInput)
    {

        cutPlane.transform.eulerAngles += new Vector3(0, 0, -AxisInput.x * cutPlanSpeed);

    }
    public SlicedHull SliceObject(GameObject obj, Material crossMaterial = null)
    {

        if (obj.GetComponent<MeshFilter>() == null) return null;
        return obj.Slice(cutPlane.transform.position, cutPlane.transform.up, crossMaterial);
    }
    public override void AimButton()
    {
 
        cutPlane.transform.localEulerAngles = Vector3.zero;

        cutPlane.SetActive(true);
        slashCam.SetActive(false);
        
        aimSlashCam.SetActive(true);

        PlayerStats.bladeMode = true;
        float timeScale = PlayerStats.bladeMode ? 0.2f : 1;
        DOVirtual.Float(Time.timeScale, timeScale, .01f, SetTimeScale);

    }
    public override void UpdateState(Vector2 v2)
    {
        if (PlayerStats.bladeMode)
        {

            if (PlayerStats.bladeMode)
            {
                RotatePlan(v2);
            }
        }
    }


    public override void ReleaseAim()
    {
        cutPlane.SetActive(false);
        slashCam.SetActive(true);
        aimSlashCam.SetActive(false);
        PlayerStats.bladeMode = false;
        float timeScale = PlayerStats.bladeMode ? 0.2f : 1;
        DOVirtual.Float(Time.timeScale, timeScale, .01f, SetTimeScale);
    }



    void SetTimeScale(float time)
    {
        Time.timeScale = time;
    }

}