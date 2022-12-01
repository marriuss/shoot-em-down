public class PlayerData
{
    public string CurrentWeaponName;
    public string[] Weapons;
    public int Money;

    public PlayerData(string currentWeaponName, string[] weapons, int money)
    {
        CurrentWeaponName = currentWeaponName;
        Weapons = weapons;
        Money = money;
    }
}