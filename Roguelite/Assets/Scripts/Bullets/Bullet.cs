using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 10f;
    public AudioClip fireSound; // Sonido al disparar
    public AudioClip impactarAlEnemigo;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        // Reproducir el sonido del disparo
        if (fireSound != null)
        {
            AudioManager.instance.ReproducirSonido(fireSound);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D hitInfo = collision.collider;

        // Comprobamos si la bala ha chocado con un enemigo
        if (hitInfo.gameObject.CompareTag("Enemigo"))
        {
            // Si choca contra un enemigo, llamamos al método TomarDaño del enemigo con la cantidad de daño que queremos aplicar
            Enemigo1IA enemy = hitInfo.GetComponent<Enemigo1IA>();
            if (enemy != null)
            {
                enemy.TomarDaño(damage);
                AudioManager.instance.ReproducirSonido(impactarAlEnemigo);
            }
        }

        // Luego, destruimos la bala, independientemente de si chocó con un enemigo o cualquier otro objeto que no sea el jugador
        if (!hitInfo.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
