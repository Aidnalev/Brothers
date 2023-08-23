using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 5f;
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1.0f;  // Tiempo de pausa entre dashes

    [Header("References")]
    public Camera cam;
    public Transform gunTransform;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movimiento;
    private bool isDashing;
    private float dashTimeLeft;
    private bool canDash = true;  // Controlar la disponibilidad del dash

    private void Start()
    {
        CheckRequiredComponents();
    }

    private void Update()
    {
        HandleRotationTowardsMouse();

        // Lógica para actualizar animaciones
        animator.SetFloat("Horizontal", movimiento.x);
        animator.SetFloat("Vertical", movimiento.y);
        animator.SetFloat("Speed", movimiento.sqrMagnitude);  // Cambiado "Velocidad" a "Speed"
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void CheckRequiredComponents()
    {
        if (cam == null) Debug.LogError("Camera is missing.");
        if (gunTransform == null) Debug.LogError("Gun transform is missing.");
        if (rb == null) Debug.LogError("Rigidbody2D is missing.");
        if (animator == null) Debug.LogError("Animator is missing.");
    }

    private void HandleRotationTowardsMouse()
    {
        if (cam != null && gunTransform != null)
        {
            Vector2 direccion = cam.ScreenToWorldPoint(Input.mousePosition) - gunTransform.position;
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            gunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angulo));
        }
    }

    public void SetMovement(float horizontal, float vertical)
    {
        movimiento.x = horizontal;
        movimiento.y = vertical;
        movimiento.Normalize();
    }

    private void MoveCharacter()
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
                rb.velocity = movimiento * speed;
            }
        }
        else
        {
            rb.velocity = movimiento * speed;
        }
    }

    public void Dash()
    {
        if (canDash && !isDashing)
        {
            StartDashing();
        }
    }

    private void StartDashing()
    {
        isDashing = true;
        canDash = false;
        dashTimeLeft = dashTime;

        StartCoroutine(DashCooldownRoutine());
    }

    private IEnumerator DashCooldownRoutine()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
