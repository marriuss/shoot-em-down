using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Player : ResetableMonoBehaviour
{
    [SerializeField] private Weapon _startWeapon;
    [SerializeField] private PlayerDataLoader _playerDataLoader;
    [SerializeField] private PlayerDataSaver _playerDataSaver;

    private int _levelMoney;
    private int _totalMoney;
    private HashSet<Weapon> _weapons = new HashSet<Weapon>();

    public event UnityAction<int> TotalMoneyChanged;
    public event UnityAction<Collider> ShotCollider;
    public event UnityAction<int> CollectedMoney;

    public Weapon CurrentWeapon { get; private set; }
    public bool HasWeapon(Weapon weapon) => _weapons.Contains(weapon);
    public bool CanBuy(Weapon weapon) => _totalMoney >= weapon.Cost;

    private void Awake()
    {
        ChangeTotalMoney(_playerDataLoader.Money);
        _weapons = _playerDataLoader.PlayerWeapons;
        Weapon currentWeapon = _playerDataLoader.CurrentPlayerWeapon;

        if (currentWeapon == null)
        {
            currentWeapon = _startWeapon;
            AddWeapon(currentWeapon);
        }

        EquipWeapon(currentWeapon);
    }

    public override void SetStartState()
    {
        _levelMoney = 0;
        CurrentWeapon.SetStartState();
    }

    public void SpawnWeapon(Vector3 position)
    {
        CurrentWeapon.Translate(position);
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.ShotCollider -= OnShotCollider;
            CurrentWeapon.HitCollider -= OnWeaponHitCollider;
        }

        if (HasWeapon(weapon) == false)
            AddWeapon(weapon);

        CurrentWeapon = weapon;
        weapon.ShotCollider += OnShotCollider;
        weapon.HitCollider += OnWeaponHitCollider;
    }

    public void BuyWeapon(Weapon weapon)
    {
        ChangeTotalMoney(_totalMoney - weapon.Cost);
        AddWeapon(weapon);
    }

    public void SaveLevelProgress()
    {
        ChangeTotalMoney(_totalMoney + _levelMoney);
        _levelMoney = 0;
        HashSet<string> weaponsNames = _weapons.Select(weapon => weapon.Name).ToHashSet();
        _playerDataSaver.SaveData(new PlayerData(CurrentWeapon.Name, weaponsNames, _totalMoney));
    }

    private void AddWeapon(Weapon weapon)
    {
        if (HasWeapon(weapon) == false)
            _weapons.Add(weapon);
    }

    private void OnShotCollider(Collider collider)
    {
        if (collider.TryGetComponent(out Money money))
        {
            CollectMoney(money);
        }
        else
        {
            ShotCollider?.Invoke(collider);
        }
    }

    private void OnWeaponHitCollider(Collider collider)
    {
        if (collider.TryGetComponent(out Money money))
            CollectMoney(money);
    }

    private void CollectMoney(Money money)
    {
        int moneyAmount = money.Value;
        _levelMoney += moneyAmount;
        CollectedMoney?.Invoke(moneyAmount);
    }

    private void ChangeTotalMoney(int newAmount)
    {
        _totalMoney = newAmount;
        TotalMoneyChanged?.Invoke(_totalMoney);
    }
}
