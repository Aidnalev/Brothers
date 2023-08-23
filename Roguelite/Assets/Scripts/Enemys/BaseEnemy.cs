using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float detectionRange = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f; // Disparos por segundo.
    public float health = 100f;
    public AudioClip shootSound; // Sonido al disparar.

    private Transform playerTransform;
    private bool playerDetected;
    private float nextFireTime = 0f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        DetectPlayer();
        if (playerDetected)
        {
            ShootAtPlayer();
        }
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= detectionRange)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }
    }

    void ShootAtPlayer()
    {
        if (Time.time > nextFireTime)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            nextFireTime = Time.time + 1f / fireRate;

            if (shootSound != null)
            {
                AudioManager.instance.ReproducirSonido(shootSound);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
