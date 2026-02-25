using UnityEngine;
using UnityEngine.VFX;
public class SplashTrigger: MonoBehaviour
{
    public GameObject SplashVfx;
    public float Mag;
    public float cooldown;
    public float offset;
    float timer;
    private void OnTriggerStay(Collider other)
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        float Mov = other.attachedRigidbody ? other.attachedRigidbody.linearVelocity.magnitude : 1f;

        if (Mov > Mag && timer <= 0)
        {
            float surfaceY = transform.position.y + GetComponent<Collider>().bounds.extents.y;
            Vector3 spawnPos = new Vector3(other.transform.position.x, surfaceY+offset, other.transform.position.z);

            GameObject splashIns = Instantiate(SplashVfx, spawnPos, Quaternion.identity);
            Destroy(splashIns, 0.5f);
            timer = cooldown;
        
    }
    }
}
