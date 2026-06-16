using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Pause : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;

    [Header("Scene")]
    public string namaSceneMainMenu = "MainMenu";

    [Header("Delay")]
    public float sceneLoadDelay = 0.15f;

    bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TombolPause();
    }

    public void TombolPause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        PlayKlikSFX();

        if (pausePanel != null)
            pausePanel.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        PlayKlikSFX();

        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void MainMenu()
    {
        StartCoroutine(MainMenuDenganSFX());
    }

    IEnumerator MainMenuDenganSFX()
    {
        PlayKlikSFX();

        Time.timeScale = 1f;

        yield return new WaitForSecondsRealtime(sceneLoadDelay);

        SceneManager.LoadScene(namaSceneMainMenu);
    }

    void PlayKlikSFX()
    {
        if (SFXManager.Instance != null)
            SFXManager.Instance.MainkanSFXKlikTombol();
    }
}