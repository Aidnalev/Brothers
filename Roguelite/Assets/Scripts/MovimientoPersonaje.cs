using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    public float speed = 5f;
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    private Rigidbody2D rb;
    private Vector2 movimiento;
    private Animator animator;
    private bool isDashing;
    private float dashTimeLeft;
    private bool isMovingSoundPlaying = false;

    public AudioClip moveSound;
    public AudioClip dashSound;
    public AudioClip shootSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        movimiento = new Vector2(moveX, moveY).normalized;

        if (movimiento.sqrMagnitude > 0.01f)
        {
            animator.SetFloat("Horizontal", movimiento.x);
            animator.SetFloat("Vertical", movimiento.y);
            animator.SetFloat("Speed", movimiento.sqrMagnitude);

            if (!isMovingSoundPlaying)
            {
                AudioManager.instance.ReproducirSonidoEnBucle(moveSound);
                isMovingSoundPlaying = true;
            }
        }
        else
        {
            animator.SetFloat("Speed", 0f);

            if (isMovingSoundPlaying)
            {
                AudioManager.instance.DetenerSonido();
                isMovingSoundPlaying = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            isDashing = true;
            dashTimeLeft = dashTime;
            AudioManager.instance.ReproducirSonido(dashSound);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            AudioManager.instance.ReproducirSonido(shootSound);
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                rb.velocity = movimiento * dashSpeed;
                dashTimeLeft -= Time.deltaTime;
            }
            else
            {
                isDashing = false;
            }
        }
        else
        {
            rb.velocity = movimiento * speed;
        }
    }
}
