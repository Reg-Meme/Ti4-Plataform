using UnityEngine;
using EzySlice;
using DG.Tweening;
using Unity.VisualScripting;

public class SlashMechanic : MonoBehaviour
{
    Inputs input;
    public GameObject cutPlane;
    public GameObject slashCam;
    public GameObject aimSlashCam;
    public float cutPlanSpeed;
    public bool bladeMode;
    public LayerMask layerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        input = new Inputs();

    }
    public void OnEnable()
    {
        input.Player.Enable();
    }
    public void OnDisable()
    {
        input.Player.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        AimCamera(input);
        float timeScale = bladeMode ? 0.2f : 1;
        DOVirtual.Float(Time.timeScale, timeScale, .02f, SetTimeScale);
        if (bladeMode)
        {
            RotatePlan();
            if (input.Player.Attack.triggered)
                Slice();

        }
      
    }
    
    public void Slice()
    {
        Collider[] hits = Physics.OverlapBox(cutPlane.transform.position, new Vector3(10, 0.1f, 10), cutPlane.transform.rotation, layerMask);
        if (hits.Length <= 0) return;
        for (int i = 0; i < hits.Length; i++)
        {
            SlicedHull hull = SliceObject(hits[i].gameObject, null);
            if (hull != null)
            {
                GameObject bottom = hull.CreateLowerHull(hits[i].gameObject, null);
                GameObject top = hull.CreateUpperHull(hits[i].gameObject, null);
                AddHullComponents(top);
                AddHullComponents(bottom);
                Destroy(hits[i].gameObject);
            }
        }
    }
    public void AddHullComponents(GameObject go)
    {

        go.layer = LayerMask.NameToLayer("Cut");
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;

        rb.AddExplosionForce(100, go.transform.position, 10);
    }
    public void RotatePlan()
    {
        Vector2 AxisInput = input.Player.Look.ReadValue<Vector2>();

        cutPlane.transform.eulerAngles += new Vector3(0, 0, -AxisInput.x * cutPlanSpeed);

    }
    public SlicedHull SliceObject(GameObject obj, Material crossMaterial = null)
    {
        if (obj.GetComponent<MeshFilter>() == null) return null;
        return obj.Slice(cutPlane.transform.position, cutPlane.transform.up, crossMaterial);
    }
    void AimCamera(Inputs input)
    {
        if (input.Player.Aim.triggered) cutPlane.transform.localEulerAngles = Vector3.zero;
        if (input.Player.Aim.IsPressed())
        {
            cutPlane.SetActive(true);
            slashCam.SetActive(false);
            aimSlashCam.SetActive(true);
            bladeMode = true;
          
        }
        else
        {
            cutPlane.SetActive(false);
            slashCam.SetActive(true);
            aimSlashCam.SetActive(false);
            bladeMode = false;
        }
    }
    void SetTimeScale(float time)
    {
        Time.timeScale = time;
    }
}
