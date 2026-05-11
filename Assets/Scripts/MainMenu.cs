using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("PilihLevel");
    }

    public void OpenCredit()
    {
        creditPanel.SetActive(true);
    }
     public void CloseCredit()
    {
        creditPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("Keluar Game");
    }
}
