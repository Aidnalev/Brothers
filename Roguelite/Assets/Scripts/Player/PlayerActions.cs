using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private PlayerWeapons playerWeapons;
    private PlayerMovement playerMovement;
    private bool isUsingMelee = true;
    private GameObject nearbyWeapon; // Referencia al arma cercana detectada

    private void Start()
    {
        playerWeapons = GetComponent<PlayerWeapons>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // Movimiento
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        playerMovement.SetMovement(horizontal, vertical);

        // Dash con barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerMovement.Dash();
        }

        // Cambiar de arma activa con "Q"
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isUsingMelee = !isUsingMelee;
            playerWeapons.ToggleActiveWeapon();
        }

        // Atacar con el arma activa (Clic Izquierdo del ratón)
        if (Input.GetMouseButtonDown(0))
        {
            playerWeapons.Attack();
        }

        // Recoger arma del suelo con "E"

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Tecla E presionada");
            if (nearbyWeapon != null)
            {
                Debug.Log("Intentando recoger arma: " + nearbyWeapon.name);
                playerWeapons.PickUpWeapon(nearbyWeapon);
                nearbyWeapon = null;
            }
            else
            {
                Debug.Log("No hay arma cercana para recoger");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WeaponOnGround"))
        {
            Debug.Log("Entrando en contacto con arma: " + other.gameObject.name);
            nearbyWeapon = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("WeaponOnGround"))
        {
            Debug.Log("Dejando de estar en contacto con arma: " + other.gameObject.name);
            nearbyWeapon = null;
        }
    }
}
