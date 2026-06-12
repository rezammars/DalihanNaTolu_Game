using UnityEngine;
using UnityEngine.UI;

public class NPCStatusUI : MonoBehaviour
{
    [Header("UI")]
    public Text nameText;
    public Text roleText;
    public Text statusText;

    [Header("Data")]
    public string npcName;
    public string roleName;
    public int emotionValue;

    public void Setup(string newName, string newRole, int startEmotion)
    {
        npcName = newName;
        roleName = newRole;
        emotionValue = startEmotion;

        UpdateUI();
    }

    public void AddEmotion(int amount)
    {
        emotionValue += amount;
        emotionValue = Mathf.Clamp(emotionValue, -2, 2);

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (nameText != null)
            nameText.text = npcName;

        if (roleText != null)
            roleText.text = roleName;

        if (statusText != null)
            statusText.text = GetStatusName();
    }

    string GetStatusName()
    {
        if (emotionValue <= -2)
            return "Marah";

        if (emotionValue == -1)
            return "Kesal";

        if (emotionValue == 0)
            return "Netral";

        if (emotionValue == 1)
            return "Senang";

        return "Puas";
    }
}