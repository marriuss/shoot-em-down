using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Magazine
{
    private const float ReloadingTimeModifier = 0.25f;

    public event UnityAction<float> AmountChanged;

    public float CurrentAmount { get; private set; }
    public int MaxAmount { get; private set; }
    public int MinAmount { get; private set; }
    public bool IsReloading { get; private set; }
    public bool IsEmpty => CurrentAmount == MinAmount;

    public Magazine(int maxValue)
    {
        MinAmount = 0;
        MaxAmount = maxValue;
        Fill();
    }

    public void TakeBullet(int amount)
    {
        ChangeValue(CurrentAmount - amount);
    }

    public void Fill()
    {
        ChangeValue(MaxAmount);
    }

    public IEnumerator Reload()
    {
        IsReloading = true;
        float reloadingTime = (MaxAmount - CurrentAmount) * ReloadingTimeModifier;
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        float newValue;

        while (CurrentAmount < MaxAmount)
        {
            newValue = Mathf.MoveTowards(CurrentAmount, MaxAmount, reloadingTime * Time.deltaTime);
            ChangeValue(newValue); 
            yield return waitForEndOfFrame;
        }

        IsReloading = false;
    }

    private void ChangeValue(float newValue)
    {
        CurrentAmount = newValue;
        AmountChanged?.Invoke(newValue);
    }

}
