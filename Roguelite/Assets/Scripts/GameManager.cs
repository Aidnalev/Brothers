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
            public HUDCtroler hudCtroler; // Añade una referencia pública al controlador de HUD

 
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
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        if(currentHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
    public void RecuperarVida(float vida)
    {
        currentHealth += vida; // Aumenta la salud actual por la cantidad especificada
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth); // Asegura que la salud actual no exceda la salud máxima
        hudCtroler.UpdateHealthBar(); // Llama a la función para actualizar la barra de salud en la IU
    }

    public int GetScore()
    {
        return score;
    }

    public void AddPoints(int value)
    {
        score += value;
    }
}
