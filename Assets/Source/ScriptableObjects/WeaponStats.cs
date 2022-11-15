using UnityEngine;

[CreateAssetMenu(menuName ="SO/Create new/Weapon stats")]
public class WeaponStats : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _magazineCapacity;
    [SerializeField] private float _shootingDelay;
    [SerializeField] private int _cost;
    [SerializeField] private Sprite _sprite;

    public string Name => _name;
    public int MagazineCapacity => _magazineCapacity;
    public float ShootingDelay => _shootingDelay;
    public int Cost => _cost;
    public Sprite Sprite => _sprite;
}
