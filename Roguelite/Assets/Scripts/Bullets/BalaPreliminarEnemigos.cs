using UnityEngine;

public class BalaPreliminarEnemigos : MonoBehaviour
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
            Debug.LogError("Rigidbody2D es nulo en la bala preliminar del enemigo!");
            return;
        }

        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D hitInfo = collision.collider;

        // Comprobamos si la bala ha chocado con el jugador
        if (hitInfo.gameObject.CompareTag("Player"))
        {
            // Si choca contra el jugador, dañamos al jugador y reproducimos el sonido de golpe
            GameManager.instance.TakeDamage(damage);
            if (hitSound != null)
            {
                AudioManager.instance.ReproducirSonido(hitSound);
            }
        }

        // Luego, destruimos la bala, independientemente de si chocó con el jugador o cualquier otro objeto que no sea un enemigo
        if (!hitInfo.gameObject.CompareTag("Enemigo"))
        {
            Destroy(gameObject);
        }
    }
}
