using UnityEngine;

public class EnemyRanged : EnemyBase
{
    public Transform bulletSpawnpoint;
    public float bulletSpeed = 5f;

    protected override void PerformAttack()
    {
        if (playerTransform == null || bulletSpawnpoint == null || enemyData.bulletType == null) return;

        Vector3 direction = (playerTransform.position - bulletSpawnpoint.position).normalized;
        GameObject bullet = ObjectPool.Instance.Spawn(enemyData.bulletType, bulletSpawnpoint.position, Quaternion.LookRotation(direction));

        if (bullet.TryGetComponent(out EnemyBulletManager bulletManager))
            bulletManager.Initialize(enemyData);

        if (bullet.TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(direction * bulletSpeed, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        if (playerTransform == null) return;

        float preferredDistance = enemyData.attackRange;
        float stopThreshold = 0.5f;
        float moveSpeed = enemyData.movementSpeed;

        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float distance = directionToPlayer.magnitude;

        if (Mathf.Abs(distance - preferredDistance) > stopThreshold)
        {
            float moveDir = distance > preferredDistance ? 1f : -1f;
            transform.position += directionToPlayer.normalized * moveSpeed * moveDir * Time.deltaTime;
        }

        Vector3 lookDirection = directionToPlayer;
        lookDirection.y = 0f;
        if (lookDirection != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * 5f);
    }
}