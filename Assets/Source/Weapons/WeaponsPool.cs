using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New WeaponsPool", menuName ="SO/WeaponsPool")]
public class WeaponsPool : ScriptableObject
{
    [SerializeField] private List<Weapon> _weapons;

    public List<Weapon> Weapons => new List<Weapon>(_weapons);

    public Weapon FindByName(string name)
    {
        return _weapons.Find(weapon => weapon.Info.Name == name);
    }
}
