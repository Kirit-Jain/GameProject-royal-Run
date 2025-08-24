using UnityEngine;

public class PlayerCollisionHandller : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float hitCooldown = 1f;
    [SerializeField] float adjustChangeMoveSpeed = -2f;

    const string hitString = "Hit";

    LevelGenerator levelGenerator;

    float coolDownTimer = 0;

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        if (levelGenerator == null)
            Debug.LogError("No Level Generator found in scene, please add one");
    }

    void Update()
    {
        coolDownTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (coolDownTimer < hitCooldown) return;

        levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeed);
        animator.SetTrigger(hitString);
        coolDownTimer = 0;
    }
}
