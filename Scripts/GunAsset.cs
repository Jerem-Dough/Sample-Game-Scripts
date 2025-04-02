using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewGameAsset", menuName = "Scriptable Assets/Gun Asset")]
public class GunAsset : ScriptableObject
{
    [Header("General Properties")]
    public string assetName;
    public Sprite icon;
    public GameObject bulletType;


    [Header("Gun Stats")]
    public float damage;
    public enum FireRate {whole, half, quarter, eighth};
    public FireRate fireRate;
    public enum BulletBehavior {singleShot, scatter, lazer, multishot};
    public BulletBehavior bulletBehavior;
    public List<AudioClip> gunAudios;
}
