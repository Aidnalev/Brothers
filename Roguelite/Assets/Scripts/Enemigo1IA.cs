using UnityEngine;

public class Enemigo1IA : MonoBehaviour
{
    public float velocidad = 2f;
    public float vida = 10f;
    public float da�oColision = 20f; // Da�o al colisionar con el jugador
    public float da�oDisparo = 5f; // Da�o del disparo
    public float rangoAtaque = 10f; // Aument� esto
    public float tiempoEntreDisparos = 1f;
    public float tiempoDeEnfriamiento = 5f;
    public GameObject balaPrefab;
    public AudioClip collisionSound;
    public AudioClip MuerteDelEnemigo;

    private Transform jugador;
    private Rigidbody2D rb;
    private float siguienteDisparo = 0f;
    private float siguienteAtaque = 0f;
    private float siguienteGolpe = 0f;
    private float enfriamientoGolpe = 1f;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        rb.MovePosition(rb.position + direccion * velocidad * Time.fixedDeltaTime);
        Atacar();
    }

    private void Atacar()
    {
        if (Time.time > siguienteAtaque)
        {
            if (Time.time > siguienteDisparo)
            {
                siguienteDisparo = Time.time + tiempoEntreDisparos;
                Disparar();
            }
        }
    }

    private void Disparar()
    {
        GameObject bala = Instantiate(balaPrefab, transform.position, transform.rotation);
        Bullet1Enemigo scriptBala = bala.GetComponent<Bullet1Enemigo>();
        if (scriptBala != null)
        {
            Vector2 direccion = (jugador.position - transform.position).normalized;
            scriptBala.SetDirection(direccion); // Ajusta la direcci�n de la bala
            scriptBala.damage = da�oDisparo; // Configura el da�o
        }
    }

    public void TomarDa�o(float cantidad)
    {
        vida -= cantidad;

        // Reproduce el sonido de recibir un disparo
        if (MuerteDelEnemigo != null)
        {
            
        }

        if (vida <= 0)
        {
            Destroy(gameObject);
            AudioManager.instance.ReproducirSonido(MuerteDelEnemigo);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time > siguienteGolpe)
        {
            siguienteGolpe = Time.time + enfriamientoGolpe;
            HUDCtroler hud = FindObjectOfType<HUDCtroler>();
            if (hud != null)
            {
                hud.TakeDamage(da�oColision);
                AudioManager.instance.ReproducirSonido(collisionSound);
            }
        }
    }
}
