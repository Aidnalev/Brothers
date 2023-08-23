using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float currentHealth = 100f;
    public float MaxHealth = 100f;
    public float CurrentHealth { get { return currentHealth; } }

    public static GameManager instance { get; private set; }
    private static int score;
    public int Score { get { return score; } }
    public HUDCtroler hudCtroler;

    public static int flechas = 0;
    public static int polvora = 0;
    public int Flechas { get { return flechas; } }
    public int Polvora { get { return polvora; } }

     private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        score = 0;
        currentHealth = MaxHealth;
        hudCtroler.UpdateHealthBar(); // Aseguramos que se actualice la salud al inicio
        hudCtroler.UpdateAmmo(); // Aseguramos que se actualice la munición al inicio
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        hudCtroler.UpdateHealthBar();

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void RecuperarVida(float vida)
    {
        currentHealth += vida;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        hudCtroler.UpdateHealthBar();
    }

    public int GetScore()
    {
        return score;
    }

    public void AddPoints(int value)
    {
        score += value;
        hudCtroler.UpdateScore(); // Actualizamos el score en la HUD cada vez que cambie
    }

    public void AddArrowAmmo(int cantidad)
    {
        flechas += cantidad;
        flechas = Mathf.Clamp(flechas, 0, 100);
        hudCtroler.UpdateAmmo(); // Actualizamos la munición en la HUD cada vez que cambie
    }

    public void AddGunpowderAmmo(int cantidad)
    {
        polvora += cantidad;
        polvora = Mathf.Clamp(polvora, 0, 100);
        hudCtroler.UpdateAmmo(); // Actualizamos la munición en la HUD cada vez que cambie
    }
}
