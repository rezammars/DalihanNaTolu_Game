using UnityEngine;
using UnityEngine.UI;

public class MisiLevel1 : MonoBehaviour
{
    [Header("Flow")]
    public FlowPanel flowPanel;

    [Header("NPC")]
    public NPCInteract tetua;
    public NPCInteract hula;
    public NPCInteract dongan;
    public NPCInteract boru;

    [Header("UI")]
    public Text teksMisi;

    bool hulaDone;
    bool donganDone;
    bool boruDone;

    int misiTahap;

    void Start()
    {
        ResetMisi();
    }

    void ResetMisi()
    {
        misiTahap = 0;

        hulaDone = false;
        donganDone = false;
        boruDone = false;

        tetua.SetInteractable(false);
        hula.SetInteractable(false);
        dongan.SetInteractable(false);
        boru.SetInteractable(false);

        SetMisi("");
    }

    void SetMisi(string text)
    {
        if (teksMisi != null)
            teksMisi.text = text;
    }

    // =========================
    // Dialog Awal Selesai
    // =========================
    public void OnDialogAwalSelesai()
    {
        flowPanel.ShowStep(1);

        misiTahap = 1;

        tetua.SetInteractable(true);

        SetMisi("Bicaralah dengan tetua adat mengenai musyawarah");
    }

    // =========================
    // Tetua Pertama
    // =========================
    public void OnTetuaPertamaSelesai()
    {
        if (misiTahap != 1)
            return;

        misiTahap = 2;

        hula.SetInteractable(true);
        dongan.SetInteractable(true);
        boru.SetInteractable(true);

        SetMisi("Bicaralah dengan semua orang untuk mengetahui posisi mereka");
    }

    // =========================
    // Hula
    // =========================
    public void OnHulaSelesai()
    {
        Debug.Log("Hula bicara");
        if (misiTahap != 2)
            return;

        hulaDone = true;

        CekTigaNPC();
    }

    // =========================
    // Dongan
    // =========================
    public void OnDonganSelesai()
    {
        Debug.Log("Dongan bicara");
        if (misiTahap != 2)
            return;

        donganDone = true;

        CekTigaNPC();
    }

    // =========================
    // Boru Pertama
    // =========================
    public void OnBoruPertamaSelesai()
    {
        Debug.Log("Boru bicara");
        if (misiTahap != 2)
            return;

        boruDone = true;

        CekTigaNPC();
    }

    void CekTigaNPC()
    {
        if (!hulaDone || !donganDone || !boruDone) return;

        misiTahap = 3;

        tetua.SetInteractable(true);
        hula.SetInteractable(true);
        dongan.SetInteractable(true);
        boru.SetInteractable(true);

        tetua.directEvent = true;
        tetua.eventType = NPCInteract.EventType.TetuaPuzzlePeran;

        SetMisi("Kembali temui tetua adat. Atur tempat duduk untuk semua orang (Bicaralah dengan mereka untuk petunjuk)");
    }

    // =========================
    // Tetua Kedua
    // =========================
    public void OnTetuaBukaPuzzlePeran()
    {
        if (misiTahap != 3)
            return;

        flowPanel.ShowStep(2);
    }

    // =========================
    // Puzzle Peran Selesai
    // =========================
    public void OnPuzzlePeranSelesai()
    {
        flowPanel.ShowStep(1);

        misiTahap = 4;

        tetua.SetInteractable(false);
        hula.SetInteractable(false);
        dongan.SetInteractable(false);

        boru.SetInteractable(true);

        boru.directEvent = true;
        boru.eventType = NPCInteract.EventType.BoruUlos;

        SetMisi("Temui Anak Boru");
    }

    // =========================
    // Boru Kedua
    // =========================
    public void OnBoruUlos()
    {
        if (misiTahap != 4)
            return;

        misiTahap = 5;

        SetMisi("");

        // Dialog Awal Ulos
        flowPanel.ShowStep(3);
    }

    // =========================
    // Dialog Awal Ulos Selesai
    // =========================
    public void OnDialogAwalUlosSelesai()
    {
        flowPanel.ShowStep(4);
    }

    // =========================
    // Puzzle Ulos Selesai
    // =========================
    public void OnPuzzleUlosSelesai()
    {
        flowPanel.ShowStep(5);
    }

    // =========================
    // Dialog Akhir Selesai
    // =========================
    public void OnDialogAkhirSelesai()
    {
        flowPanel.ShowStep(6);
    }
}