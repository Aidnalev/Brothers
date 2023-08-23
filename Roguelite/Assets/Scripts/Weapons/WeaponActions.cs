using System.Collections;
using UnityEngine;

public class WeaponActions : MonoBehaviour
{
    private WeaponStats weaponStats;
    private Weapon weapon;

    // Variables para armas a distancia.
    public GameObject projectilePrefab;

    // Variables para armas melé.
    public float meleeAttackDuration = 0.3f;  // Duración del ataque melé.
    public float hitDelay = 0.2f; // Retraso antes de detectar golpes
    private bool isMeleeAttacking = false;
    public GameObject bloodEffect; // Efecto de sangre cuando se daña un enemigo con arma melé.

    public AudioClip swingSound; // Sonido del swing del arma
    public AudioClip hitSound; // Sonido cuando el arma golpea

    private void Awake()
    {
        weaponStats = GetComponent<WeaponStats>();
        weapon = GetComponent<Weapon>();
    }

    public void Attack()
    {
        if (weapon.weaponCategory == Weapon.WeaponCategory.Ranged)
        {
            RangedAttack();
        }
        else if (weapon.weaponCategory == Weapon.WeaponCategory.Melee)
        {
            MeleeAttack();
        }
    }

    private void RangedAttack()
    {
        Transform playerFirePoint = GameObject.FindGameObjectWithTag("Player").transform.Find("gun/FirePoint");

        if (projectilePrefab && playerFirePoint)
        {
            GameObject projectile = Instantiate(projectilePrefab, playerFirePoint.position, playerFirePoint.rotation);

            if (projectile)
            {
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                float spread = Random.Range(-weaponStats.bulletSpread, weaponStats.bulletSpread);
                Vector2 calculatedVelocity = Quaternion.Euler(0, 0, spread) * playerFirePoint.right;
                rb.velocity = calculatedVelocity * weaponStats.range;
            }
        }
    }

    private void MeleeAttack()
    {
        if (isMeleeAttacking) return;

        isMeleeAttacking = true;

        // Reproducir sonido de swing
        AudioSource.PlayClipAtPoint(swingSound, transform.position);

        // Comienza una corrutina para manejar el delay antes de detectar enemigos
        StartCoroutine(DetectAndDamageEnemies());
    }

    private IEnumerator DetectAndDamageEnemies()
    {
        // Espera el tiempo definido antes de detectar enemigos
        yield return new WaitForSeconds(hitDelay);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, weaponStats.range);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                // Aplicando el daño al enemigo
                enemy.GetComponent<BaseEnemy>().TakeDamage(weaponStats.damage);

                // Reproducir sonido de golpe
                AudioSource.PlayClipAtPoint(hitSound, enemy.transform.position);

                // Efecto de sangre
                Instantiate(bloodEffect, enemy.transform.position, Quaternion.identity);
            }
        }

        // Finaliza el ataque melé
        FinishMeleeAttack();
    }

    private void FinishMeleeAttack()
    {
        isMeleeAttacking = false;
    }
}
