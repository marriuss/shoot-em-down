using UnityEngine;

public static class TimeChanger
{
    private const float GameTime = 1;
    private const float FrozenTime = 0;

    public static bool IsTimeFrozen => Time.timeScale == FrozenTime;

    public static void FrozeTime()
    {
        SetTime(FrozenTime);
    }

    public static void UnfrozeTime()
    {
        SetTime(GameTime);
    }

    private static void SetTime(float value)
    {
        Time.timeScale = value;
    }
}
