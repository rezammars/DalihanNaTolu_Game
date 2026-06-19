using UnityEngine;
using UnityEngine.UI;

public class IndexManager : MonoBehaviour
{
    [System.Serializable]
    public class IndexData
    {
        public string nama;

        [TextArea(3, 8)]
        public string deskripsi;

        public Button button;
        public Image iconImage;
        public GameObject lockOverlay;
    }

    [System.Serializable]
    public class IndexUnlockGroup
    {
        public string groupName;
        public int[] indexToUnlock;
    }

    [Header("Panel")]
    public GameObject indexPanel;

    [Header("Info UI")]
    public Text namaText;
    public Text deskripsiText;

    [Header("Data Index")]
    public IndexData[] indexData;

    [Header("Unlock Group")]
    public IndexUnlockGroup[] unlockGroups;

    void Start()
    {
        if (indexPanel != null)
            indexPanel.SetActive(false);

        SetupButtons();
        RefreshIndexUI();

        if (namaText != null)
            namaText.text = "Index Budaya";

        if (deskripsiText != null)
            deskripsiText.text = "Pilih ikon yang sudah terbuka untuk melihat informasi.";
    }

    void SetupButtons()
    {
        for (int i = 0; i < indexData.Length; i++)
        {
            int index = i;

            if (indexData[i].button != null)
            {
                indexData[i].button.onClick.RemoveAllListeners();
                indexData[i].button.onClick.AddListener(() => TampilkanIndex(index));
            }
        }
    }

    public void BukaIndex()
    {
        PlayClickSFX();

        if (indexPanel != null)
            indexPanel.SetActive(true);

        RefreshIndexUI();
    }

    public void TutupIndex()
    {
        PlayClickSFX();

        if (indexPanel != null)
            indexPanel.SetActive(false);
    }

    public void TampilkanIndex(int index)
    {
        PlayClickSFX();

        if (index < 0 || index >= indexData.Length)
            return;

        if (!IsIndexUnlocked(index))
        {
            if (namaText != null)
                namaText.text = "Terkunci";

            if (deskripsiText != null)
                deskripsiText.text = "Selesaikan stage atau mini game tertentu untuk membuka informasi ini.";

            return;
        }

        if (namaText != null)
            namaText.text = indexData[index].nama;

        if (deskripsiText != null)
            deskripsiText.text = indexData[index].deskripsi;
    }

    public void UnlockIndex(int index)
    {
        if (index < 0 || index >= indexData.Length)
            return;

        PlayerPrefs.SetInt("Index_" + index, 1);
        PlayerPrefs.Save();

        RefreshIndexUI();
    }

    public void UnlockGroup(int groupIndex)
    {
        if (unlockGroups == null || unlockGroups.Length == 0)
            return;

        if (groupIndex < 0 || groupIndex >= unlockGroups.Length)
            return;

        for (int i = 0; i < unlockGroups[groupIndex].indexToUnlock.Length; i++)
        {
            int index = unlockGroups[groupIndex].indexToUnlock[i];

            if (index >= 0 && index < indexData.Length)
            {
                PlayerPrefs.SetInt("Index_" + index, 1);
            }
        }

        PlayerPrefs.Save();
        RefreshIndexUI();

        Debug.Log("Index group terbuka: " + unlockGroups[groupIndex].groupName);
    }

    public void RefreshIndexUI()
    {
        for (int i = 0; i < indexData.Length; i++)
        {
            bool unlocked = IsIndexUnlocked(i);

            if (indexData[i].lockOverlay != null)
                indexData[i].lockOverlay.SetActive(!unlocked);

            if (indexData[i].iconImage != null)
            {
                if (unlocked)
                    indexData[i].iconImage.color = Color.white;
                else
                    indexData[i].iconImage.color = new Color(0.35f, 0.35f, 0.35f, 1f);
            }
        }
    }

    bool IsIndexUnlocked(int index)
    {
        return PlayerPrefs.GetInt("Index_" + index, 0) == 1;
    }

    public void ResetIndex()
    {
        for (int i = 0; i < indexData.Length; i++)
        {
            PlayerPrefs.DeleteKey("Index_" + i);
        }

        PlayerPrefs.Save();
        RefreshIndexUI();

        if (namaText != null)
            namaText.text = "Index Budaya";

        if (deskripsiText != null)
            deskripsiText.text = "Semua index telah dikunci kembali.";
    }

    void PlayClickSFX()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayKlikTombol();
    }
}