using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;
    public int skillPoints = 0;

    public void AddExperience(int xpAmount) // Called from the enemy script when an enemy is killed
    {
        currentXP += xpAmount;

        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        skillPoints++;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.2f); // XP curve
        Debug.Log($"Level up! Now at level {currentLevel}. Skill points: {skillPoints}"); // Can be removed later once we have level text and skill trees set up
    }
}

