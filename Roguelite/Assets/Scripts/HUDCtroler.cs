using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDCtroler : MonoBehaviour
{
    public Image coinImage;
    public TextMeshProUGUI scoreText;
    public Image healthBar;

    private void Start()
    {
        UpdateHealthBar();
        Debug.Log("Health Bar en Start: " + healthBar);
        UpdateScore();
    }

       public void UpdateHealthBar()
    {
        Debug.Log("Health Bar en Start: " + healthBar);
        if (healthBar == null)
        {
            Debug.LogError("Health bar is not assigned!");
            Debug.Log("Health bar object: " + healthBar);
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

    private void UpdateScore()
    {
        scoreText.text = GameManager.instance.GetScore().ToString();
    }

    private void Update()
    {
        UpdateScore();
    }
}
