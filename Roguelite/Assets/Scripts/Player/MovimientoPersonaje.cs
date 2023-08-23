using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    // Variables públicas
    [Header("Settings")]
    public float speed = 5f;
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;

    [Header("References")]
    public Camera cam;
    public Transform gunTransform;
    public Rigidbody2D rb;
    public Animator animator;

    // Variables privadas
    private Vector2 movimiento;
    private bool isDashing;
    private float dashTimeLeft;

    private void Start()
    {
        CheckRequiredComponents();
    }

    private void Update()
    {
        HandleRotationTowardsMouse();
        HandlePlayerInput();
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

    private void HandlePlayerInput()
    {
        // Captura de movimiento del personaje
        movimiento.x = Input.GetAxisRaw("Horizontal");
        movimiento.y = Input.GetAxisRaw("Vertical");
        movimiento.Normalize();  // Esto asegura que la diagonal no sea más rápida

        // Dashing
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartDashing();
        }
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
            }
        }
        else
        {
            rb.velocity = movimiento * speed;
        }
    }

    private void StartDashing()
    {
        isDashing = true;
        dashTimeLeft = dashTime;

        // TODO: Llamar al AudioManager aquí o al CharacterAudioController cuando se implemente
    }
}
