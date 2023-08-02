using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomarVidaPollo : MonoBehaviour
{
    public float recuperarVida = 20f; //vida recuperada
    public AudioClip cogerPollo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.instance.CurrentHealth < GameManager.instance.MaxHealth)
        {
            // Calcula la cantidad de vida que se recuperará, sin exceder la salud máxima
            float vidaARecuperar = Mathf.Min(recuperarVida, GameManager.instance.MaxHealth - GameManager.instance.CurrentHealth);

            GameManager.instance.RecuperarVida(vidaARecuperar);
            AudioManager.instance.ReproducirSonido(cogerPollo);
            Destroy(this.gameObject);
        }
    }
}
