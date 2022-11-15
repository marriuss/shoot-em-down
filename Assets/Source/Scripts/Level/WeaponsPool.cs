using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponsPool : MonoBehaviour
{
    private List<Weapon> _weapons;

    private void Awake()
    {
        _weapons = GetWeaponsList();
    }

    public Weapon FindByName(string name)
    {
        return _weapons.Find(weapon => weapon.name == name);
    }

    public List<Weapon> GetWeaponsList()
    {
        return GetComponentsInChildren<Weapon>().ToList();
    }
}
