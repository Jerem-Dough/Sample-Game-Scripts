using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour
{
    public float lifeTime = 2f; // Set your desired lifetime in seconds
    private float damage;
    void OnEnable()
    {
        // Start the despawn coroutine when the bullet is enabled
        StartCoroutine(DespawnAfterTime());
    }
    public void Initialize(GunAsset gunAsset)
    {
        damage = gunAsset.damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyBase enemy))
        {
            enemy.TakeDamage(damage);
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

