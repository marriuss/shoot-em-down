public class PlayerDataObject
{
    public string CurrentWeaponName;
    public string[] Weapons;
    public int Money;

    public PlayerDataObject(string currentWeaponName, string[] weapons, int money)
    {
        CurrentWeaponName = currentWeaponName;
        Weapons = weapons;
        Money = money;
    }
}