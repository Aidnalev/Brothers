using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDCtroler : MonoBehaviour
{
    public Image coinImage;
    public TextMeshProUGUI scoreText;
    public Image healthBar;
    public TextMeshProUGUI flechasText;
    public TextMeshProUGUI polvoraText;

    private void Start()
    {
        UpdateHealthBar();
        UpdateScore();
        UpdateAmmo();
    }

    public void UpdateHealthBar()
    {
        if (healthBar == null)
        {
            Debug.LogError("Health bar is not assigned!");
            return;
        }

        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager is not assigned!");
            return;
        }

        healthBar.fillAmount = GameManager.instance.CurrentHealth / GameManager.instance.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        GameManager.instance.TakeDamage(damage);
        UpdateHealthBar();
    }

    public void UpdateScore()
    {
        scoreText.text = GameManager.instance.GetScore().ToString();
    }

    public void UpdateAmmo()
    {
        flechasText.text = GameManager.instance.Flechas.ToString();
        polvoraText.text = GameManager.instance.Polvora.ToString();
    }

    // Ya no necesitamos el Update para esto
    //private void Update()
    //{
    //    UpdateScore();
    //    UpdateAmmo();
    //}
}
