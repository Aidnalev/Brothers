using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public enum ActiveWeaponType { None, Melee, Ranged }

    [Header("Melee Weapons")]
    public GameObject lanzaPrefab;
    public GameObject garrotePrefab;
    public GameObject dagaPrefab;
    public GameObject espadaPrefab;
    public GameObject macanaPrefab;

    [Header("Ranged Weapons")]
    public GameObject arcoPrefab;
    public GameObject dardosPrefab;
    public GameObject mosquetePrefab;
    public GameObject pistolaPrefab;
    public GameObject arcabuzPrefab;

    private MeleeWeaponType currentMeleeWeaponType = MeleeWeaponType.None;
    private RangedWeaponType currentRangedWeaponType = RangedWeaponType.None;

    private GameObject currentMeleeWeapon;
    private GameObject currentRangedWeapon;

    private Weapon currentMeleeWeaponComponent;
    private Weapon currentRangedWeaponComponent;
    private WeaponStats currentMeleeWeaponStats;
    private WeaponStats currentRangedWeaponStats;

    public ActiveWeaponType activeWeaponType = ActiveWeaponType.None;

    public void EquipMeleeWeapon(MeleeWeaponType weaponType)
    {
        Debug.Log("Equipando arma melé: " + weaponType.ToString());

        if (currentMeleeWeapon != null)
        {
            DropWeapon(currentMeleeWeapon);
            currentMeleeWeaponType = MeleeWeaponType.None;
        }

        switch (weaponType)
        {
            case MeleeWeaponType.Lanza:
                currentMeleeWeapon = Instantiate(lanzaPrefab, transform.position, Quaternion.identity, transform);
                break;
            case MeleeWeaponType.Garrote:
                currentMeleeWeapon = Instantiate(garrotePrefab, transform.position, Quaternion.identity, transform);
                break;
            case MeleeWeaponType.Daga:
                currentMeleeWeapon = Instantiate(dagaPrefab, transform.position, Quaternion.identity, transform);
                break;
            case MeleeWeaponType.Espada:
                currentMeleeWeapon = Instantiate(espadaPrefab, transform.position, Quaternion.identity, transform);
                break;
            case MeleeWeaponType.Macana:
                currentMeleeWeapon = Instantiate(macanaPrefab, transform.position, Quaternion.identity, transform);
                break;
        }
        currentMeleeWeaponType = weaponType;

        currentMeleeWeaponComponent = currentMeleeWeapon.GetComponent<Weapon>();
        currentMeleeWeaponStats = currentMeleeWeapon.GetComponent<WeaponStats>();
    }

    public void EquipRangedWeapon(RangedWeaponType weaponType)
    {
        Debug.Log("Equipando arma a distancia: " + weaponType.ToString());

        if (currentRangedWeapon != null)
        {
            DropWeapon(currentRangedWeapon);
            currentRangedWeaponType = RangedWeaponType.None;
        }

        switch (weaponType)
        {
            case RangedWeaponType.Arco:
                currentRangedWeapon = Instantiate(arcoPrefab, transform.position, Quaternion.identity, transform);
                break;
            case RangedWeaponType.Dardos:
                currentRangedWeapon = Instantiate(dardosPrefab, transform.position, Quaternion.identity, transform);
                break;
            case RangedWeaponType.Mosquete:
                currentRangedWeapon = Instantiate(mosquetePrefab, transform.position, Quaternion.identity, transform);
                break;
            case RangedWeaponType.Pistola:
                currentRangedWeapon = Instantiate(pistolaPrefab, transform.position, Quaternion.identity, transform);
                break;
            case RangedWeaponType.Arcabuz:
                currentRangedWeapon = Instantiate(arcabuzPrefab, transform.position, Quaternion.identity, transform);
                break;
        }
        currentRangedWeaponType = weaponType;

        currentRangedWeaponComponent = currentRangedWeapon.GetComponent<Weapon>();
        currentRangedWeaponStats = currentRangedWeapon.GetComponent<WeaponStats>();
    }

    public void ToggleActiveWeapon()
    {
        Debug.Log("Cambiando arma activa.");

        switch (activeWeaponType)
        {
            case ActiveWeaponType.None:
                if (currentMeleeWeapon != null)
                {
                    activeWeaponType = ActiveWeaponType.Melee;
                }
                else if (currentRangedWeapon != null)
                {
                    activeWeaponType = ActiveWeaponType.Ranged;
                }
                break;
            case ActiveWeaponType.Melee:
                if (currentRangedWeapon != null)
                {
                    activeWeaponType = ActiveWeaponType.Ranged;
                }
                break;
            case ActiveWeaponType.Ranged:
                if (currentMeleeWeapon != null)
                {
                    activeWeaponType = ActiveWeaponType.Melee;
                }
                break;
        }
    }

    public void PickUpWeapon(GameObject weaponToPickUp)
    {
        Debug.Log("Intentando recoger el arma: " + weaponToPickUp.name);

        Weapon weaponInfo = weaponToPickUp.GetComponent<Weapon>();
        if (weaponInfo != null)
        {
            if (weaponInfo.weaponCategory == Weapon.WeaponCategory.Melee)
            {
                EquipMeleeWeapon(weaponInfo.meleeType);
            }
            else
            {
                EquipRangedWeapon(weaponInfo.rangedType);
            }
            Destroy(weaponToPickUp);
            ToggleActiveWeapon();
        }
        else
        {
            Debug.LogError("El objeto recogido no tiene un componente Weapon: " + weaponToPickUp.name);
        }
    }

    private void DropWeapon(GameObject weapon)
    {
        Debug.Log("Soltando arma: " + weapon.name);
        weapon.transform.parent = null;
        weapon.transform.position = transform.position + transform.forward * 0.5f;
    }

    public void Attack()
    {
        Debug.Log("Intentando atacar.");

        switch (activeWeaponType)
        {
            case ActiveWeaponType.None:
                Debug.LogWarning("No hay arma equipada para atacar.");
                break;
            case ActiveWeaponType.Melee:
                if (currentMeleeWeapon != null)
                {
                    currentMeleeWeapon.GetComponent<WeaponActions>().Attack();
                }
                else
                {
                    Debug.LogWarning("Intentando atacar con arma melé pero no hay arma melé equipada.");
                }
                break;
            case ActiveWeaponType.Ranged:
                if (currentRangedWeapon != null)
                {
                    currentRangedWeapon.GetComponent<WeaponActions>().Attack();
                }
                else
                {
                    Debug.LogWarning("Intentando atacar a distancia pero no hay arma a distancia equipada.");
                }
                break;
        }
    }
}
