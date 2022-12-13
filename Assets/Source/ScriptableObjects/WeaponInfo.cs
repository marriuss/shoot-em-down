using UnityEngine;

[CreateAssetMenu(menuName ="SO/Create new/Weapon info")]
public class WeaponInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _cost;
    [SerializeField] private Sprite _sprite;

    public string Name => _name;
    public int Cost => _cost;
    public Sprite Sprite => _sprite;
}
