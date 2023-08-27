using UnityEngine;

[CreateAssetMenu(fileName = "NewRangeWeapon", menuName = "Custom/RangeWeapon")]
public class RangeWeapon : ScriptableObject
{
    public string weaponName;
    public int damage;
    public float range;
    public float attackSpeed;
    public Sprite weaponSprite;
}
