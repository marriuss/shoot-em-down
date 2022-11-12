using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health
{
    public event UnityAction<int> ValueChanged;

    public int CurrentValue { get; private set; }
    public int MaxValue { get; private set; }
    public int MinValue { get; private set; }

    public Health(int maxValue)
    {
        MinValue = 0;
        MaxValue = maxValue;
        Heal();
    }

    public void ApplyDamage(int damage)
    {
        ChangeValue(CurrentValue - damage);
    }

    public void Heal()
    {
        ChangeValue(MaxValue);
    }

    private void ChangeValue(int newValue)
    {
        CurrentValue = newValue;
        ValueChanged?.Invoke(newValue);
    }
}
