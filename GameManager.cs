using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    [Header("UI Bağlantıları")]
    public TextMeshProUGUI scoreText;   
    public TextMeshProUGUI livesText; // YENİ: Hak yazısı
    public TextMeshProUGUI resultText;  
    public GameObject gameOverPanel;    

    [Header("Oyun Durumu")]
    public int score = 0;
    public int lives = 3; // YENİ: Başlangıç hakkı

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 1; 
        UpdateLivesUI(); // Oyun başında yazıyı güncelle
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null) scoreText.text = "Score: " + score;
    }

    // YENİ: Hak kaybetme fonksiyonu
    public void LoseLife()
    {
        lives--; // Hakkı 1 azalt
        UpdateLivesUI();

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            // Hak kaldıysa sahneyi yenile (Veya sadece gemiyi resetle)
            // Basitlik için sahneyi yeniliyoruz ama skor ve can sıfırlanmasın diye
            // 'DontDestroyOnLoad' kullanmak gerekir. 
            // Başlangıç seviyesi için: Sadece gemiyi tekrar aktif edelim.
            // (Bu kısmı PlayerHealth içinde halledeceğiz)
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = "Lives: " + lives;
    }

    public void WinGame()
    {
        ShowPanel("YOU WIN!");
    }

    public void GameOver()
    {
        ShowPanel("GAME OVER");
    }

    void ShowPanel(string message)
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (resultText != null) resultText.text = message;
        }
        Time.timeScale = 0; 
    }

    public void RestartGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
