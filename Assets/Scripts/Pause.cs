using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Pause : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelPause;
    public GameObject panelAudio;
    public GameObject panelIndex;

    [Header("Scene")]
    public string namaSceneMainMenu = "MainMenu";

    [Header("Delay")]
    public float sceneLoadDelay = 0.15f;

    bool isPaused = false;

    void Start()
    {
        if (panelPause != null)
            panelPause.SetActive(false);
        
        if (panelAudio != null)
            panelAudio.SetActive(false);
        
        if (panelIndex != null)
            panelIndex.SetActive(false);

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

        if (panelPause != null)
            panelPause.SetActive(true);

        if (panelAudio != null)
            panelAudio.SetActive(false);

        if (panelIndex != null)
            panelIndex.SetActive(false);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        PlayKlikSFX();

        if (panelPause != null)
            panelPause.SetActive(false);
        
        if (panelAudio != null)
            panelAudio.SetActive(false);

        if (panelIndex != null)
            panelIndex.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void BukaAudio()
    {
        PlayKlikSFX();

        if (panelPause != null)
            panelPause.SetActive(false);

        if (panelAudio != null)
            panelAudio.SetActive(true);

        if (panelIndex != null)
            panelIndex.SetActive(false);
    }

    public void BukaIndex()
    {
        PlayKlikSFX();

        if (panelPause != null)
            panelPause.SetActive(false);

        if (panelAudio != null)
            panelAudio.SetActive(false);

        if (panelIndex != null)
            panelIndex.SetActive(true);
    }

    public void KembaliKePause()
    {
        PlayKlikSFX();

        if (panelPause != null)
            panelPause.SetActive(true);

        if (panelAudio != null)
            panelAudio.SetActive(false);

        if (panelIndex != null)
            panelIndex.SetActive(false);
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
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayKlikTombol();
    }
}