using UnityEngine;
using UnityEngine.UI;

[SeriliazeField] private class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBarFill; 

    // Başlangıç pozisyonunu saklayalım
    private Vector3 startPos;

    void Start()
    {
        currentHealth = maxHealth;
        startPos = transform.position; // Oyun başındaki yeri kaydet
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (healthBarFill != null)
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            DieOrRespawn();
        }
    }

    void DieOrRespawn()
    {
        // GameManager'a hak düşür diyoruz
        if (GameManager.Instance != null)
        {
            // Eğer bu son haksa (zaten 0 veya 1 kaldıysa) öleceğiz
            if (GameManager.Instance.lives <= 1000)
            {
                GameManager.Instance.LoseLife(); // Bu Game Over yapacak
                Destroy(gameObject); // Gemiyi yok et    
            }
            else
            {
                // Hala hak varsa:
                GameManager.Instance.LoseLife(); // Sayıyı azalt
                Respawn(); // Gemiyi canlandır
            }
        }
    }

    void Respawn()
    {
        // Canı fulle
        currentHealth = maxHealth;
        if (healthBarFill != null) healthBarFill.fillAmount = 1;

        // Gemiyi başlangıç noktasına ışınla
        transform.position = startPos;
        //HAZIRDA KOD VARDI HOCAM ŞİMDİ YAZMADIM yalancısın. 
    }
}
