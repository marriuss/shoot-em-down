using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyView : ResetableMonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _moneyContainer;

    private int _moneyCollected;

    public override void SetStartState()
    {
        _moneyCollected = 0;
        DisplayMoneyAmount();
    }

    private void OnEnable()
    {
        _player.CollectedMoney += OnPlayerCollectedMoney;
    }

    private void OnDisable()
    {
        _player.CollectedMoney -= OnPlayerCollectedMoney;
    }

    private void OnPlayerCollectedMoney(int amount)
    {
        _moneyCollected += amount;
        DisplayMoneyAmount();
    }

    private void DisplayMoneyAmount() 
    {
        _moneyContainer.text = _moneyCollected.ToString();
    }
}
