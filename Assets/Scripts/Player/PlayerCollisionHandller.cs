using UnityEngine;

public class PlayerCollisionHandller : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] float hitCooldown = 1f;

    const string hitString = "Hit";

    float coolDownTimer = 0;

    void Update()
    {
        coolDownTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (coolDownTimer < hitCooldown) return;
        animator.SetTrigger(hitString);
        
        coolDownTimer = 0;
    }
}
