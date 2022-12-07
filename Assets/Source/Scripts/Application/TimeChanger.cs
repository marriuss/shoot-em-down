using UnityEngine;

public static class TimeChanger
{
    private const float GameTime = 1;
    private const float FrozenTime = 0;

    public static bool IsPaused => Time.timeScale != GameTime;

    public static void FrozeTime()
    {
        SetTime(FrozenTime);
    }

    public static void UnfrozeTime()
    {
        SetTime(GameTime);
    }

    public static void SetTimeScale(float time)
    {
        SetTime(time);
    }

    private static void SetTime(float value)
    {
        Time.timeScale = value;
    }
}
