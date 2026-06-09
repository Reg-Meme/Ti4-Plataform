using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    [Header("Tempo")]
    public float delayBeforeFall = 1.5f;
    public float shakeDuration = 1f;
    public float resetTime = 3f;

    [Header("Tremida")]
    public float shakeIntensity = 0.1f;
    public AudioSource audio;
    private Vector3 startPosition;
    private Rigidbody rb;
    private bool activated = false;

    void Start()
    {
        startPosition = transform.position;

        rb = GetComponent<Rigidbody>();

        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();

        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;
            StartCoroutine(FallRoutine());
        }
    }

    IEnumerator FallRoutine()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeIntensity;
            transform.position = startPosition + randomOffset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition;

        yield return new WaitForSeconds(delayBeforeFall);

        rb.isKinematic = false;
        rb.useGravity = true;

        yield return new WaitForSeconds(resetTime);

        rb.isKinematic = true;
        rb.useGravity = false;

        transform.position = startPosition;
        activated = false;
    }
}