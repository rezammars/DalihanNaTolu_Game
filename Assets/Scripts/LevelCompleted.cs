using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    public int nextLevel = 2;

    public void CompleteLevel()
    {
        int unlockedLevel =
        PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (nextLevel > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", nextLevel);

            PlayerPrefs.Save();
        }

        SceneManager.LoadScene("PilihLevel");
    }
}
