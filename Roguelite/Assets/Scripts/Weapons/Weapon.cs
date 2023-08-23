using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponCategory weaponCategory;
    public MeleeWeaponType meleeType;
    public RangedWeaponType rangedType;

    public enum WeaponCategory
    {
        Melee,
        Ranged
    }

    void Start()
    {
        // Validación: Asegurarnos de que los tipos de armas no estén configurados de manera incorrecta
        if (weaponCategory == WeaponCategory.Melee)
        {
            rangedType = RangedWeaponType.None;
        }
        else if (weaponCategory == WeaponCategory.Ranged)
        {
            meleeType = MeleeWeaponType.None;
        }
    }
}
