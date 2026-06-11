using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowPanel : MonoBehaviour
{
    [Header("Panels")]
    public GameObject[] panels;

    [Header("Start Setting")]
    public bool showPanelOnEnable = true;
    public int startPanelIndex = 0;

    [Header("Level Finish")]
    public int unlockLevelAfterFinish = 3;
    public string levelSelectSceneName = "LevelSelect";

    void OnEnable()
    {
        if (showPanelOnEnable)
        {
            ShowPanel(startPanelIndex);
        }
    }

    public void ShowPanel(int index)
    {
        if (panels == null || panels.Length == 0)
        {
            Debug.LogWarning("Panel belum diisi di " + gameObject.name);
            return;
        }

        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i] != null)
            {
                panels[i].SetActive(i == index);
            }
        }

        Debug.Log(gameObject.name + " membuka panel index: " + index);
    }

    public void FinishLevel()
    {
        Time.timeScale = 1f;

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (unlockLevelAfterFinish > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", unlockLevelAfterFinish);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene(levelSelectSceneName);
    }
}