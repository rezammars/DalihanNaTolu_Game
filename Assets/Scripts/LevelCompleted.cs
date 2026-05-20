using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    public static LevelCompleted instance;
    int npcTalked = 0;

    void Awake()
    {
        instance = this;
    }

    public void NPCFinished()
    {
        npcTalked++;

        Debug.Log("NPC selesai: " + npcTalked);

        if (npcTalked >= 3)
        {
            Debug.Log("Semua NPC selesai, pindah ke puzzle");
            SceneManager.LoadScene("PuzzlePeran");
        }
    }
}
