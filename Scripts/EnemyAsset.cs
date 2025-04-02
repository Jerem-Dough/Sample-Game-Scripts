using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Scriptable Assets/Enemy Asset")]
public class EnemyAsset : ScriptableObject
{
    [Header("General Properties")]
    public string assetName;
    public Sprite icon;

    [Header("Enemy Stats")]
    public float enemyHealth;
    public float attackPower;
    public enum attack { whole, half, quarter, eighth };
    public attack attackRate;
    public float attackRange;
    public List<AudioClip> attackAudios;
    public GameObject bulletType;

    public float movementSpeed;
    public int experienceReward;
}
