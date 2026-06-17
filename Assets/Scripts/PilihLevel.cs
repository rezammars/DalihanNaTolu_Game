using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PilihLevel : MonoBehaviour
{
    [Header("Scene Main Menu")]
    public string namaSceneMainMenu = "MainMenu";

    [Header("Nama Scene Setiap Stage")]
    public string namaSceneStage1 = "Stage1";
    public string namaSceneStage2 = "Stage2";
    public string namaSceneStage3 = "Stage3";
    public string namaSceneStage4 = "Stage4";

    [Header("Tombol Stage")]
    public Button tombolStage1;
    public Button tombolStage2;
    public Button tombolStage3;
    public Button tombolStage4;

    [Header("Delay")]
    public float sceneLoadDelay = 0.15f;

    int unlockedLevel;

    void Start()
    {
        Time.timeScale = 1f;

        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        Debug.Log("UnlockedLevel sekarang: " + unlockedLevel);

        UpdateLevelButtons();

    }

    void UpdateLevelButtons()
    {
        if (tombolStage1 != null)
        {
            tombolStage1.gameObject.SetActive(true);
            tombolStage1.interactable = true;
        }

        if (tombolStage2 != null)
        {
            tombolStage2.gameObject.SetActive(true);
            tombolStage2.interactable = unlockedLevel >= 2;
        }

        if (tombolStage3 != null)
        {
            tombolStage3.gameObject.SetActive(true);
            tombolStage3.interactable = unlockedLevel >= 3;
        }

        if (tombolStage4 != null)
        {
            tombolStage4.gameObject.SetActive(true);
            tombolStage4.interactable = unlockedLevel >= 4;
        }
    }

    public void PilihStage1()
    {
        StartCoroutine(LoadSceneDenganSFX(namaSceneStage1));
    }

    public void PilihStage2()
    {
        if (unlockedLevel >= 2)
            StartCoroutine(LoadSceneDenganSFX(namaSceneStage2));
    }

    public void PilihStage3()
    {
        if (unlockedLevel >= 3)
            StartCoroutine(LoadSceneDenganSFX(namaSceneStage3));
    }

    public void PilihStage4()
    {
        if (unlockedLevel >= 4)
            StartCoroutine(LoadSceneDenganSFX(namaSceneStage4));
    }

    public void KembaliKeMainMenu()
    {
        StartCoroutine(LoadSceneDenganSFX(namaSceneMainMenu));
    }

    IEnumerator LoadSceneDenganSFX(string sceneName)
    {
        PlayClickSFX();

        yield return new WaitForSecondsRealtime(sceneLoadDelay);

        SceneManager.LoadScene(sceneName);
    }

    void PlayClickSFX()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayKlikTombol();
    }

    public void ResetProgress()
    {
        PlayClickSFX();

        PlayerPrefs.DeleteKey("UnlockedLevel");
        PlayerPrefs.Save();

        unlockedLevel = 1;
        UpdateLevelButtons();

    }
}