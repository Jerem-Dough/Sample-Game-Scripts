using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int roomWidth = 20;
    public int roomHeight = 20;

    public string roomID = "RoomX_North";
    public string roomTag = "Combat";

    public Dictionary<string, Door> doors = new Dictionary<string, Door>
    {
        {"North", new Door(true, false)},
        {"South", new Door(false, false)},
        {"East", new Door(false, true)},
        {"West", new Door(false, false)}
    };

    public int difficultyLevel = 1;

    public List<string> enemyTable = new List<string>();
    public List<string> lootTable = new List<string>();
    public List<string> rareLootTable = new List<string> { "LegendaryWeapon", "EpicArmor", "Artifact", "SpecialItem" };

    void Start()
    {
        AssignMobsAndLoot();
    }

    void AssignMobsAndLoot()
    {
        if (roomTag == "Combat")
        {
            enemyTable.Add("BasicEnemy");
            if (difficultyLevel >= 2) enemyTable.Add("StrongerEnemy");
            if (difficultyLevel >= 3) enemyTable.Add("EliteEnemy");

            lootTable.Add("SmallHealthPotion");
            lootTable.Add("Ammo");

            if (Random.value < 0.15f)
            {
                int rareItemsToSpawn = Random.Range(1, 5);
                for (int i = 0; i < rareItemsToSpawn; i++)
                {
                    lootTable.Add(rareLootTable[Random.Range(0, rareLootTable.Count)]);
                }
            }
        }
        else if (roomTag == "Boss1")
        {
            enemyTable.Add("BossMonster");

            lootTable.Add("RareWeapon");
            lootTable.Add("LargeHealthPotion");

            if (Random.value < 0.30f)
            {
                int rareItemsToSpawn = Random.Range(1, 5);
                for (int i = 0; i < rareItemsToSpawn; i++)
                {
                    lootTable.Add(rareLootTable[Random.Range(0, rareLootTable.Count)]);
                }
            }
        }
        else if (roomTag == "Puzzle")
        {
            enemyTable.Clear();
            lootTable.Add("KeyItem");
        }
        else if (roomTag == "Checkpoint")
        {
            enemyTable.Clear();
            lootTable.Add("ShopAccess");
        }
    }
}

public class Door
{
    public bool exists;
    public bool isLocked;

    public Door(bool exists, bool isLocked)
    {
        this.exists = exists;
        this.isLocked = isLocked;
    }
}