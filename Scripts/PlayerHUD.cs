using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("Health Bar")]
    public Image healthBarFill;
    public PlayerHealth playerHealth;

    [Header("XP Bar")]
    public Image xpBarFill;
    public PlayerLevelManager levelManager;

    private void Update()  
    {
        UpdateHealthBar(); // Should probably be called directly from any damage causing source
        UpdateXPBar(); // Should probably be called directly from enemy script when xp is added
    }

    private void UpdateHealthBar()
    {
        if (playerHealth != null)
        {
            float fillAmount = playerHealth.currentHealth / playerHealth.maxHealth;
            healthBarFill.fillAmount = Mathf.Clamp01(fillAmount);
        }
    }

    private void UpdateXPBar()
    {
        if (levelManager != null)
        {
            float fillAmount = (float)levelManager.currentXP / levelManager.xpToNextLevel;
            xpBarFill.fillAmount = Mathf.Clamp01(fillAmount);
        }
    }
}
