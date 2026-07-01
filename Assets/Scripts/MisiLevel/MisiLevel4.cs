using UnityEngine;
using UnityEngine.UI;

public class MisiLevel4 : MonoBehaviour
{
    [Header("Flow")]
    public FlowPanel flowPanel;

    [Header("NPC")]
    public NPCInteract tetua;

    [Header("UI")]
    public Text teksMisi;

    int misiTahap;

    void Start()
    {
        ResetMisi();
    }

    void ResetMisi()
    {
        misiTahap = 0;

        tetua.SetInteractable(false);

        SetMisi("");
    }

    void SetMisi(string text)
    {
        if (teksMisi != null)
            teksMisi.text = text;
    }

    // Cutscene selesai
    public void OnCutsceneSelesai()
    {
        flowPanel.ShowStep(1);

        misiTahap = 1;

        tetua.SetInteractable(true);

        SetMisi(" Acara adat akan segera dimulai, tanyakan tetua apa yang harus kamu lakukan");
    }

    // Interaksi Tetua
    public void OnDialogTetua()
    {
        if (misiTahap != 1)
            return;

        flowPanel.ShowStep(2);
    }

    // Panel dialog selesai
    public void OnDialogTetuaSelesai()
    {
        misiTahap = 2;

        flowPanel.ShowStep(3);
    }

    // Puzzle Peran selesai
    public void OnPuzzlePeranSelesai()
    {
        flowPanel.ShowStep(4);
    }

    // Puzzle Urutan selesai
    public void OnPuzzleUrutanSelesai()
    {
        flowPanel.ShowStep(5);
    }

    // Puzzle Keseimbangan selesai
    // Hasil akan diatur oleh Stage4Hasil
}