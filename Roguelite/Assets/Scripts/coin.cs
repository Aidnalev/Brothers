using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;  // Asigna un valor a la moneda

       public AudioClip coinSound;  // Agrega una variable para el sonido de la moneda

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.AddPoints(value);  // Actualiza el puntaje usando el método AddPoints del GameManager

            AudioManager.instance.ReproducirSonido(coinSound);  // Reproduce el sonido de la moneda

            Destroy(gameObject);  // Destruye la moneda
        }
    }
}
