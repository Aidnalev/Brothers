using UnityEngine;

public class Bullet1Enemigo : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 5f;
    public AudioClip hitSound; // Sonido al golpear al jugador.

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D es nulo en la bala del enemigo!");
            return;
        }

        rb.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            // Reproduce el sonido de golpe
            if (hitSound != null)
            {
                AudioManager.instance.ReproducirSonido(hitSound);
            }

            // Lógica para dañar al jugador
            GameManager.instance.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
