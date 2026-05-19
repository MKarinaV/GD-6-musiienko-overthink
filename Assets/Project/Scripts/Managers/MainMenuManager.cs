using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject howToPlayPanel;
    public GameObject settingsPanel;

    public Slider volumeSlider;

    void Start()
    {
        // START VOLUME
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 1f);

        AudioListener.volume = savedVolume;

        // PANELS
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }

        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(false);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        // SLIDER
        if (volumeSlider != null)
        {
            volumeSlider.minValue = 0.1f;
            volumeSlider.maxValue = 1f;

            volumeSlider.value = savedVolume;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenHowToPlay()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }

        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(true);
        }
    }

    public void CloseHowToPlay()
    {
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(false);
        }

        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }
    }

    public void OpenSettings()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }
    }

    public void ChangeVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.1f, 1f);

        AudioListener.volume = volume;

        PlayerPrefs.SetFloat("GameVolume", volume);

        PlayerPrefs.Save();
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit Game");
    }
}