using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public EnemyAsset enemyData;
    protected float health;
    public AudioSource audioSource;
    protected Transform playerTransform;
    protected PlayerLevelManager playerLevelManager;
    private int currentAudioIndex = 0;

    protected virtual void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            playerTransform = playerObj.transform;
    }

    protected virtual void Start()
    {
        health = enemyData.enemyHealth;
        playerLevelManager = FindAnyObjectByType<PlayerLevelManager>();
    }

    protected virtual void OnEnable()
    {
        BeatManager.Instance?.RegisterEnemy(this);
    }

    protected virtual void OnDisable()
    {
        BeatManager.Instance?.UnregisterEnemy(this);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
            Die();
    }

    protected virtual void Die()
    {
        playerLevelManager?.AddExperience(enemyData.experienceReward);
        ObjectPool.Instance.Despawn(gameObject);
    }

    public void HandleBeat()
    {
        int beat = BeatManager.Instance.beatCounter;
        bool shouldAttack =
            (enemyData.attackRate == EnemyAsset.attack.whole && beat % 8 == 0) ||
            (enemyData.attackRate == EnemyAsset.attack.half && beat % 4 == 0) ||
            (enemyData.attackRate == EnemyAsset.attack.quarter && beat % 2 == 0) ||
            (enemyData.attackRate == EnemyAsset.attack.eighth && beat % 1 == 0);

        if (shouldAttack)
        {
            PerformAttack();

            if (audioSource != null && enemyData.attackAudios.Count > 0)
            {
                audioSource.PlayOneShot(enemyData.attackAudios[currentAudioIndex]);
                currentAudioIndex = (currentAudioIndex + 1) % enemyData.attackAudios.Count;
            }
        }
    }

    protected abstract void PerformAttack();
}
