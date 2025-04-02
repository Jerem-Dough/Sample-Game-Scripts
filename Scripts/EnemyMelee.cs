using UnityEngine;
using System.Collections;

public class EnemyMelee : EnemyBase
{
    public float meleeDamage;
    public float growScaleMultiplier = 1.3f;
    public float growDuration = 0.25f;

    private Vector3 originalScale;

    protected override void Start()
    {
        base.Start();
        meleeDamage = enemyData.attackPower;
        originalScale = transform.localScale;
    }

    protected override void PerformAttack()
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance <= enemyData.attackRange + .1)
        {
            Debug.Log($"{name} melee attack hits player for {meleeDamage} damage");

            if (playerTransform.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(meleeDamage);
            }

            StartCoroutine(GrowTemporarily());
        }
    }

    IEnumerator GrowTemporarily()
    {
        Vector3 grownScale = originalScale * growScaleMultiplier;
        transform.localScale = grownScale;

        yield return new WaitForSeconds(growDuration);

        transform.localScale = originalScale;
    }

    private void Update()
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance > enemyData.attackRange)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            float moveSpeed = enemyData.movementSpeed;
            transform.position += direction * moveSpeed * Time.deltaTime;

            direction.y = 0f;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);
        }
        else
        {
            // Optionally face the player even when idle
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.y = 0f;
            if (directionToPlayer != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToPlayer), Time.deltaTime * 5f);
        }
    }

}
