using UnityEngine;
using System.Collections;

public class EnemyBulletManager : MonoBehaviour
{
    public float lifeTime = 2f; // Set your desired lifetime in seconds
    public float damage;
    void OnEnable()
    {
        // Start the despawn coroutine when the bullet is enabled
        StartCoroutine(DespawnAfterTime());
    }
    public void Initialize(EnemyAsset enemyAsset)
    {
        damage = enemyAsset.attackPower;
    }

    private void OnCollisionEnter(Collision collision)
    {
            // Enemy bullet hits the player
            if (collision.gameObject.TryGetComponent(out PlayerHealth player))
            {
                player.TakeDamage(damage);
            }
       
        // Return bullet to the pool after hit
        gameObject.SetActive(false);
    }

    IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        ObjectPool.Instance.Despawn(gameObject);
    }

    void OnDisable()
    {
        // Optionally stop any running coroutines if the bullet is deactivated early
        StopAllCoroutines();
    }
}
