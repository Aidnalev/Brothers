using UnityEngine;

public class Enemigo1IA : MonoBehaviour
{
    public float velocidad = 2f;
    public float vida = 10f;
    public float dañoColision = 20f;
    public float dañoDisparo = 5f;
    public float rangoAtaque = 10f;
    public float tiempoEntreDisparos = 1f;
    public GameObject balaPrefab; // Prefab directo
    public AudioClip collisionSound;
    public AudioClip MuerteDelEnemigo;
    public AudioClip shootSound;

    private Transform jugador;
    private Rigidbody2D rb;
    private float siguienteDisparo = 0f;
    private Vector2 direccion; // Almacenaremos la dirección una vez para optimizar

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        direccion = (jugador.position - transform.position).normalized;
        rb.MovePosition(rb.position + direccion * velocidad * Time.fixedDeltaTime);
        Atacar();
    }

    private void Atacar()
    {
        if (Time.time > siguienteDisparo)
        {
            siguienteDisparo = Time.time + tiempoEntreDisparos;
            Disparar();
        }
    }

    private void Disparar()
    {
        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y + 0.5f);
        GameObject bala = Instantiate(balaPrefab, spawnPosition, Quaternion.identity); // Usando el prefab directo y agregando un desplazamiento en y

        BalaPreliminarEnemigos scriptBala = bala.GetComponent<BalaPreliminarEnemigos>();
        if (scriptBala != null)
        {
            scriptBala.SetDirection(direccion);
            scriptBala.damage = dañoDisparo;
        }

        // Reproducimos el sonido de disparo
        if (shootSound != null)
        {
            AudioManager.instance.ReproducirSonido(shootSound);
        }
    }

    public void TomarDaño(float cantidad)
    {
        vida -= cantidad;

        if (vida <= 0)
        {
            AudioManager.instance.ReproducirSonido(MuerteDelEnemigo);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HUDCtroler hud = FindObjectOfType<HUDCtroler>();
            if (hud != null)
            {
                hud.TakeDamage(dañoColision);
                AudioManager.instance.ReproducirSonido(collisionSound);
            }
        }
    }
}
