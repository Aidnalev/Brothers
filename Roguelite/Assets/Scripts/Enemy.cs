using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 20f; // Cantidad de daño que este enemigo inflige
    public AudioClip collisionSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Supongamos que tu jugador tiene una etiqueta "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            HUDCtroler hud = FindObjectOfType<HUDCtroler>();
            if (hud != null)
            {
                hud.TakeDamage(damage);
                AudioManager.instance.ReproducirSonido(collisionSound);
            }
        }
    }
}
