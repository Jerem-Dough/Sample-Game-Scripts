using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Scriptable Assets/Character Asset")]
public class CharacterAsset : ScriptableObject
{
    [Header("General Properties")]
    public string assetName;
    public Sprite icon;

    [Header("Character Stats")]
    public int health;
    public float speed;
}
