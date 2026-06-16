using UnityEngine;
using UnityEngine.UI;

public class IndexManager : MonoBehaviour
{
    [System.Serializable]
    public class DataIndex
    {
        public string nama;

        [TextArea(3, 8)]
        public string deskripsi;
    }

    [Header("Panel")]
    public GameObject panelIndex;

    [Header("Info UI")]
    public Text teksNama;
    public Text teksDeskripsi;

    [Header("Data Index")]
    public DataIndex[] dataIndex;

    void Start()
    {
        if (panelIndex != null)
            panelIndex.SetActive(false);

        if (dataIndex != null && dataIndex.Length > 0)
            TampilkanIndexTanpaSFX(0);
    }

    public void BukaIndex()
    {
        PlayClickSFX();

        if (panelIndex != null)
            panelIndex.SetActive(true);

        if (dataIndex != null && dataIndex.Length > 0)
            TampilkanIndexTanpaSFX(0);
    }

    public void TutupIndex()
    {
        PlayClickSFX();

        if (panelIndex != null)
            panelIndex.SetActive(false);
    }

    public void TampilkanIndex(int index)
    {
        PlayClickSFX();
        SetIndexData(index);
    }

    void TampilkanIndexTanpaSFX(int index)
    {
        SetIndexData(index);
    }

    void SetIndexData(int index)
    {
        if (dataIndex == null || dataIndex.Length == 0)
            return;


        if (index < 0 || index >= dataIndex.Length)
            return;

        if (teksNama != null)
            teksNama.text = dataIndex[index].nama;

        if (teksDeskripsi != null)
            teksDeskripsi.text = dataIndex[index].deskripsi;
    }

    void PlayClickSFX()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayKlikTombol();
    }
}
