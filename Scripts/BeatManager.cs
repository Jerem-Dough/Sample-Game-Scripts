using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeatManager : MonoBehaviour
{
    public static BeatManager Instance { get; private set; }

    public PlayerAudioMechanic PAM;
    public BackgroundMusicController BackgroundMusic;

    public int beatCounter { get; private set; } = 0; // Global Beat Counter
    public float bpm = 280f; // Beats Per Minute

    private List<EnemyBase> registeredEnemies = new List<EnemyBase>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(BeatCoroutine());
    }

    IEnumerator BeatCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f / bpm);
            beatCounter++;
            PAM.OnBeat();
            BackgroundMusic.PlayBackgroundBeat();
            TriggerEnemies();
        }
    }

    public void RegisterEnemy(EnemyBase enemy)
    {
        if (!registeredEnemies.Contains(enemy))
            registeredEnemies.Add(enemy);
    }

    public void UnregisterEnemy(EnemyBase enemy)
    {
        if (registeredEnemies.Contains(enemy))
            registeredEnemies.Remove(enemy);
    }
    private void TriggerEnemies()
    {
        foreach (EnemyBase enemy in registeredEnemies)
        {
            enemy.HandleBeat();
        }
    }
}
