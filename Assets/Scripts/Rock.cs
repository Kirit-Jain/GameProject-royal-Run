using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ParticleSystem rockParticles;
    [Header("Settings")]
    [SerializeField] float shakeModifier = 10f;
    [SerializeField] float CollisionFXCooldown = 1f;

    CinemachineImpulseSource cinemachineImpulseSource;
    AudioSource audioSource;

    float cooldownTimer = 0f;

    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (cooldownTimer < CollisionFXCooldown) return;

        FireImpulse();
        CollisionFX(collision);
        cooldownTimer = 0f;
    }

    void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1 / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);

        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    void CollisionFX(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        rockParticles.transform.position = contactPoint.point;
        rockParticles.Play();
        audioSource.Play();
    }
}
