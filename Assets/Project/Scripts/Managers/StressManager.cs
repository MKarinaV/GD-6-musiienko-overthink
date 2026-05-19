using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class StressManager : MonoBehaviour
{
    [Header("UI")]
    public Slider stressBar;
    public Slider calmEnergyBar;

    public GameObject gameplayUI;
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    [Header("Game Over Text")]
    public TMP_Text distanceText;
    public TMP_Text bestText;

    [Header("Gameplay Text")]
    public TMP_Text gameplayDistanceText;
    public TMP_Text gameplayBestText;

    [Header("Effects")]
    public Image redOverlay;

    [Header("Audio")]
    public AudioSource gameplayAudio;
    public AudioSource gameOverAudio;

    [Header("Stress")]
    public float stress = 20f;
    public float maxStress = 100f;

    [Header("Calm Energy")]
    public float calmEnergy = 100f;
    public float maxCalmEnergy = 100f;

    [Header("Speeds")]
    public float stressIncreaseSpeed = 10f;
    public float calmDecreaseSpeed = 20f;

    public float energyDrainSpeed = 25f;
    public float energyRestoreSpeed = 15f;

    [Header("Distance")]
    public float distanceMultiplier = 10f;

    private float distance = 0f;
    private float bestDistance = 0f;

    private bool isGameOver = false;
    private bool isPaused = false;

    void Start()
    {
        Time.timeScale = 1;

        stressBar.minValue = 0;
        stressBar.maxValue = 1;

        calmEnergyBar.minValue = 0;
        calmEnergyBar.maxValue = 1;

        bestDistance = PlayerPrefs.GetFloat("BestDistance", 0f);

        stressBar.value = stress / maxStress;
        calmEnergyBar.value = calmEnergy / maxCalmEnergy;

        gameplayUI.SetActive(true);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);

        gameplayBestText.text =
            Mathf.FloorToInt(bestDistance) + "m";

        if (redOverlay != null)
        {
            Color color = redOverlay.color;
            color.a = 0f;
            redOverlay.color = color;
        }

        if (gameplayAudio != null)
        {
            gameplayAudio.Play();
        }

        if (gameOverAudio != null)
        {
            gameOverAudio.Stop();
        }
    }

    void Update()
    {
        
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }

        if (isGameOver || isPaused)
        {
            return;
        }

        
        distance += Time.deltaTime * distanceMultiplier;

        gameplayDistanceText.text =
            Mathf.FloorToInt(distance) + "m";

        
        bool isHolding = Keyboard.current.spaceKey.isPressed;
        bool canCalm = calmEnergy > 0;

        
        if (isHolding && canCalm)
        {
            stress -= calmDecreaseSpeed * Time.deltaTime;

            calmEnergy -= energyDrainSpeed * Time.deltaTime;
        }
        else
        {
            stress += stressIncreaseSpeed * Time.deltaTime;

            calmEnergy += energyRestoreSpeed * Time.deltaTime;
        }

        
        stress = Mathf.Clamp(stress, 0, maxStress);

        calmEnergy = Mathf.Clamp(
            calmEnergy,
            0,
            maxCalmEnergy
        );

        
        stressBar.value = stress / maxStress;

        calmEnergyBar.value =
            calmEnergy / maxCalmEnergy;

        
        if (redOverlay != null)
        {
            float stressPercent = stress / maxStress;

            Color overlayColor = redOverlay.color;

            overlayColor.a = Mathf.Lerp(
                0f,
                0.45f,
                stressPercent
            );

            redOverlay.color = overlayColor;
        }

        
        if (stress >= maxStress)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;

        
        if (distance > bestDistance)
        {
            bestDistance = distance;

            PlayerPrefs.SetFloat(
                "BestDistance",
                bestDistance
            );

            PlayerPrefs.Save();
        }

        
        distanceText.text =
            Mathf.FloorToInt(distance) + "m";

        bestText.text =
            Mathf.FloorToInt(bestDistance) + "m";

        
        if (gameplayAudio != null)
        {
            gameplayAudio.Stop();
        }

        if (gameOverAudio != null)
        {
            gameOverAudio.Play();
        }

        
        gameplayUI.SetActive(false);

        pausePanel.SetActive(false);

        gameOverPanel.SetActive(true);

        Time.timeScale = 0;
    }

    public void TogglePause()
    {
        if (isGameOver) return;

        isPaused = !isPaused;

        pausePanel.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}