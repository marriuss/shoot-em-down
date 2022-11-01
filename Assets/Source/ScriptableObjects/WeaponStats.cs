using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Create new/Weapon stats")]
public class WeaponStats : ScriptableObject
{
    [SerializeField] private int _magazineCapacity;
    [SerializeField] private float _shootingDelay;

    public int MagazineCapacity => _magazineCapacity;
    public float ShootingDelay => _shootingDelay;
}
