using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowPanel : MonoBehaviour
{
    [System.Serializable]
    public class PanelStep
    {
        public string stepName;
        public GameObject[] objectsToShow;
    }

    [Header("Panel Steps")]
    public PanelStep[] steps;

    [Header("Start Setting")]
    public bool showStepOnEnable = true;
    public int startStepIndex = 0;

    [Header("Level Finish")]
    public int unlockLevelAfterFinish = 2;
    public string levelSelectSceneName = "PilihStage";

    void OnEnable()
    {
        if (showStepOnEnable)
        {
            ShowStep(startStepIndex);
        }
    }

    public void ShowStep(int index)
    {
        if (steps == null || steps.Length == 0)
        {
            Debug.LogWarning("Steps belum diisi di " + gameObject.name);
            return;
        }

        if (index < 0 || index >= steps.Length)
        {
            Debug.LogWarning("Index step tidak valid: " + index);
            return;
        }

        for (int i = 0; i < steps.Length; i++)
        {
            if (steps[i].objectsToShow == null) continue;

            foreach (GameObject obj in steps[i].objectsToShow)
            {
                if (obj != null)
                    obj.SetActive(i == index);
            }
        }

        Debug.Log(gameObject.name + " membuka step index: " + index);
    }

    public void FinishLevel()
    {
        Time.timeScale = 1f;

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (unlockLevelAfterFinish > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", unlockLevelAfterFinish);
            PlayerPrefs.Save();

            Debug.Log("Progress disimpan. UnlockedLevel = " + unlockLevelAfterFinish);
        }
        else
        {
            Debug.Log("Progress tidak berubah. UnlockedLevel saat ini = " + unlockedLevel);
        }

        SceneManager.LoadScene(levelSelectSceneName);
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("UnlockedLevel");
        PlayerPrefs.Save();

        Debug.Log("Progress level direset.");
    }
}