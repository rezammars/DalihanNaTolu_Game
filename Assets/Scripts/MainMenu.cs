using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [Header("Scene")]
    public string pilihStageNamaScene = "PilihStage";

    [Header("Panel")]
    public GameObject creditPanel;

    [Header("Delay")]
    public float sceneLoadDelay = 0.15f;

    void Start()
    {
        Time.timeScale = 1f;

        if (creditPanel != null)
            creditPanel.SetActive(false);
    }

    public void MainGame()
    {
        StartCoroutine(LoadSceneDenganSFX(pilihStageNamaScene));
    }

    public void BukaCredit()
    {
        PlayClickSFX();

        if (creditPanel != null)
            creditPanel.SetActive(true);
    }

    public void TutupCredit()
    {
        PlayClickSFX();

        if (creditPanel != null)
            creditPanel.SetActive(false);
    }

    public void KeluarGame()
    {
        StartCoroutine(KeluarDenganSFX());
    }

    IEnumerator LoadSceneDenganSFX(string sceneName)
    {
        PlayClickSFX();

        yield return new WaitForSeconds(sceneLoadDelay);

        SceneManager.LoadScene(sceneName);
    }

    IEnumerator KeluarDenganSFX()
    {
        PlayClickSFX();

        yield return new WaitForSeconds(sceneLoadDelay);

        Application.Quit();

        Debug.Log("Keluar dari game");
    }

    void PlayClickSFX()
    {
        if (SFXManager.Instance != null)
            SFXManager.Instance.MainkanSFXKlikTombol();
    }
}