using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PilihLevel : MonoBehaviour
{
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;

    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        level2Button.interactable = unlockedLevel >= 2;
        level3Button.interactable = unlockedLevel >= 3;
        level4Button.interactable = unlockedLevel >= 4;
    }
    public void OpenLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OpenLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void OpenLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void OpenLevel4()
    {
        SceneManager.LoadScene("Level 4");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
