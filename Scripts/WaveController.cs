using UnityEngine;
using TMPro;

public class WaveController : MonoBehaviour
{
    [Header("Spawn Points")]
    public Transform[] spawnPoints = new Transform[4];

    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs;
    [Range(0f, 1f)]
    public float spawnChance = 0.5f;

    [Header("Wave Timing")]
    public float waveInterval = 30f;
    private float waveTimer;

    [Header("UI")]
    public TextMeshProUGUI waveCountdownText;

    private void Start()
    {
        waveTimer = waveInterval;
    }

    private void Update()
    {
        waveTimer -= Time.deltaTime;

        // Update countdown text
        if (waveCountdownText != null)
        {
            waveCountdownText.text = $"Next wave in: {Mathf.CeilToInt(waveTimer)}s";
        }

        if (waveTimer <= 0f)
        {
            SpawnWave();
            waveTimer = waveInterval;
        }
    }

    private void SpawnWave()
    {
        Debug.Log($"[WaveSpawner] Spawning new wave at {Time.time:F1}s");

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint == null) continue;

            if (Random.value <= spawnChance)
            {
                GameObject selectedEnemyPrefab = GetRandomEnemyPrefab();

                if (selectedEnemyPrefab != null)
                {
                    GameObject enemy = ObjectPool.Instance.Spawn(
                        selectedEnemyPrefab,
                        spawnPoint.position,
                        spawnPoint.rotation
                    );

                    enemy.SetActive(true);
                    Debug.Log($"Spawned {selectedEnemyPrefab.name} at {spawnPoint.name}");
                }
            }
        }
    }

    private GameObject GetRandomEnemyPrefab()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("WaveSpawner: No enemy prefabs assigned!");
            return null;
        }

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[randomIndex];
    }
}
