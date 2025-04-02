using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // UI elements
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    // Settings Control
    public Slider sfxSlider;
    public Slider musicSlider;
    public Slider gammaSlider;
    public Dropdown bulletVisibilityDropdown;
    public Toggle fullscreenToggle;
    public Toggle rumbleToggle;
    public Toggle foundHudToggle;

    // State Management
    private bool isPaused = false;

    // Initialize them UI and Load Settings 
    private void Start()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        LoadSettings();
    }

    // Checks for the Pause Key (Escape until rewritten for console)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    // Activate Pause Menu
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    // Close Pause Menu and Resume Game
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Open Options Menu
    public void OpenOptions()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    // Close Pause Menu
    public void CloseOptions()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    // Quit the game
    public void ExitGame()
    {
        Application.Quit();
    }

    // Handlers for the utility settings. PlayerPrefs saves the settings
    public void SetSFXVolume(float volume) { PlayerPrefs.SetFloat("SFXVolume", volume); } // Adjust the SFX volume 
    public void SetMusicVolume(float volume) { PlayerPrefs.SetFloat("MusicVolume", volume); } // Adjust the music volume
    public void SetGamma(float value) { PlayerPrefs.SetFloat("Gamma", value); } // Adjust the screen brightness
    public void SetFullscreen(bool isFullscreen) { Screen.fullScreen = isFullscreen; } // Toggle fullscreen
    public void SetRumble(bool enabled) { PlayerPrefs.SetInt("Rumble", enabled ? 1 : 0); } // Toggle controller vibration, idk if how everyone feels about this one but can remove if necessary
    public void SetBulletVisibility(int index) { PlayerPrefs.SetInt("BulletVisibility", index); } // Change bullet visibilty. A cool settings, maybe useful for players who are epileptic and weird.
    public void SetFoundHUD(bool enabled) { PlayerPrefs.SetInt("FoundHUD", enabled ? 1 : 0); } // Toggle HUD visibility


    // Loads all saved settings when the game starts
    void LoadSettings()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 5f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 5f);
        gammaSlider.value = PlayerPrefs.GetFloat("Gamma", 50f);
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        rumbleToggle.isOn = PlayerPrefs.GetInt("Rumble", 1) == 1;
        bulletVisibilityDropdown.value = PlayerPrefs.GetInt("BulletVisiblility", 1);
        foundHudToggle.isOn = PlayerPrefs.GetInt("FoundHUD", 1) == 1;
    }
}
