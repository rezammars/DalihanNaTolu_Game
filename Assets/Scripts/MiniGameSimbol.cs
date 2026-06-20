using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MiniGameSimbol : MonoBehaviour
{
    [System.Serializable]
    public class Soal
    {
        [Header("Pertanyaan")]
        [TextArea(2, 5)]
        public string teksPertanyaan;

        [Header("Gambar")]
        public Sprite gambarSimbol;

        [Header("Pilihan Jawaban")]
        public string pilihan1;
        public string pilihan2;
        public string pilihan3;
        public string pilihan4;

        [Header("Jawaban Benar")]
        public int jawabanBenarIndex;
    }

    [Header("Data Soal")]
    public Soal[] daftarSoal;

    [Header("UI Soal")]
    public Text teksPertanyaan;
    public Image gambarSimbol;

    [Header("Tombol Pilihan")]
    public Button tombolPilihan1;
    public Button tombolPilihan2;
    public Button tombolPilihan3;
    public Button tombolPilihan4;

    [Header("Teks Tombol Pilihan")]
    public Text teksPilihan1;
    public Text teksPilihan2;
    public Text teksPilihan3;
    public Text teksPilihan4;

    [Header("Hasil")]
    public GameObject panelHasil;
    public Text teksHasil;

    [Header("Aturan Skor")]
    public int skorMinimalBukaIndex = 3;

    [Header("Unlock Index")]
    public IndexManager indexManager;
    public int unlockGroupIndex = 5;

    [Header("Setelah Selesai")]
    public float delayLanjut = 2f;
    public UnityEvent onQuizFinished;

    int nomorSoal = 0;
    int skor = 0;
    bool sudahMenjawab = false;

    void OnEnable()
    {
        ResetQuiz();
    }

    void ResetQuiz()
    {
        CancelInvoke();

        nomorSoal = 0;
        skor = 0;
        sudahMenjawab = false;

        if (panelHasil != null)
            panelHasil.SetActive(false);

        SetupTombol();
        TampilkanSoal();
    }

    void SetupTombol()
    {
        tombolPilihan1.onClick.RemoveAllListeners();
        tombolPilihan2.onClick.RemoveAllListeners();
        tombolPilihan3.onClick.RemoveAllListeners();
        tombolPilihan4.onClick.RemoveAllListeners();

        tombolPilihan1.onClick.AddListener(() => PilihJawaban(0));
        tombolPilihan2.onClick.AddListener(() => PilihJawaban(1));
        tombolPilihan3.onClick.AddListener(() => PilihJawaban(2));
        tombolPilihan4.onClick.AddListener(() => PilihJawaban(3));
    }

    void TampilkanSoal()
    {
        if (daftarSoal == null || daftarSoal.Length == 0)
        {
            Debug.LogWarning("Daftar soal belum diisi.");
            return;
        }

        sudahMenjawab = false;

        Soal soal = daftarSoal[nomorSoal];

        if (teksPertanyaan != null)
            teksPertanyaan.text = soal.teksPertanyaan;

        if (gambarSimbol != null)
            gambarSimbol.sprite = soal.gambarSimbol;

        if (teksPilihan1 != null)
            teksPilihan1.text = soal.pilihan1;

        if (teksPilihan2 != null)
            teksPilihan2.text = soal.pilihan2;

        if (teksPilihan3 != null)
            teksPilihan3.text = soal.pilihan3;

        if (teksPilihan4 != null)
            teksPilihan4.text = soal.pilihan4;

        SetTombolAktif(true);
    }

    void PilihJawaban(int jawabanIndex)
    {
        if (sudahMenjawab) return;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayKlikTombol();

        sudahMenjawab = true;
        SetTombolAktif(false);

        Soal soal = daftarSoal[nomorSoal];

        if (jawabanIndex == soal.jawabanBenarIndex)
            skor++;

        Invoke(nameof(LanjutSoal), 0.7f);
    }

    void LanjutSoal()
    {
        nomorSoal++;

        if (nomorSoal >= daftarSoal.Length)
            SelesaikanQuiz();
        else
            TampilkanSoal();
    }

    void SelesaikanQuiz()
    {
        if (panelHasil != null)
            panelHasil.SetActive(true);

        if (skor >= skorMinimalBukaIndex)
        {
            if (teksHasil != null)
                teksHasil.text = "Kamu berhasil memahami simbol adat.\nSkor: " + skor + "/" + daftarSoal.Length;

            BukaIndexKhusus();
        }
        else
        {
            if (teksHasil != null)
                teksHasil.text = "Masih ada simbol adat yang perlu dipelajari.\nSkor: " + skor + "/" + daftarSoal.Length;
        }

        Invoke(nameof(LanjutSetelahQuiz), delayLanjut);
    }

    void BukaIndexKhusus()
    {
        if (indexManager != null)
            indexManager.UnlockGroup(unlockGroupIndex);
    }

    void LanjutSetelahQuiz()
    {
        onQuizFinished.Invoke();
    }

    void SetTombolAktif(bool aktif)
    {
        if (tombolPilihan1 != null)
            tombolPilihan1.interactable = aktif;

        if (tombolPilihan2 != null)
            tombolPilihan2.interactable = aktif;

        if (tombolPilihan3 != null)
            tombolPilihan3.interactable = aktif;

        if (tombolPilihan4 != null)
            tombolPilihan4.interactable = aktif;
    }
}