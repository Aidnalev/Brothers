using UnityEngine;

public class Bullet1Enemigo : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 5f;
    public AudioClip fireSound; // Sonido al disparar
    public AudioClip hitSound; // Sonido al golpear al jugador

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
        if (fireSound != null) // Reproduce el sonido del disparo
        {
            AudioManager.instance.ReproducirSonido(fireSound);
        }
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
            HUDCtroler hud = FindObjectOfType<HUDCtroler>();
            if (hud != null)
            {
                hud.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
