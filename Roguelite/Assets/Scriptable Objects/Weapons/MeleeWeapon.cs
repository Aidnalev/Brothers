using UnityEngine;

[CreateAssetMenu(fileName = "NewMeleeWeapon", menuName = "Custom/MeleeWeapon")]
public class MeleeWeapon : ScriptableObject
{
    public string weaponName;
    public int damage;
    public float range;
    public float attackSpeed;
    public Sprite weaponSprite;
}
