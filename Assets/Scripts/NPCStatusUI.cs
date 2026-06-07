using UnityEngine;
using UnityEngine.UI;

public class NPCStatusUI : MonoBehaviour
{
    public Text roleNameText;
    public Text statusText;
    public Image iconImage;

    public string npcName;
    public string roleName;

    public int emotionValue;

    public void Setup(string npc, string role, int value)
    {
        npcName = npc;
        roleName = role;
        emotionValue = value;

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
        roleNameText.text = npcName + "\n" + roleName;
        statusText.text = GetStatusName();
    }

    string GetStatusName()
    {
        if (emotionValue <= -2) return "Marah";
        if (emotionValue == -1) return "Kesal";
        if (emotionValue == 0) return "Netral";
        if (emotionValue == 1) return "Senang";
        return "Puas";
    }
}