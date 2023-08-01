using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Importamos la libreria de TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Añadimos una referencia estática a esta instancia

    private static int score;

    public TextMeshProUGUI scoreText;

    private void Awake()   // Utilizamos Awake en lugar de Start
    {
        // Comprueba si instance ya tiene una referencia
        if (instance == null)
        {
            instance = this;  // Si no, establece esta como la referencia
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // Si ya existe una referencia, destruimos esta para evitar duplicados
        }
    }

    private void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddPoints(int value)
    {
        score += value;
        scoreText.text = "Score: " + score.ToString();
    }
}
