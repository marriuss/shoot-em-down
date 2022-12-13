using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _moneyContainer;

    private void OnEnable()
    {
        _player.LevelMoneyChanged += OnPlayersLevelMoneyChanged;
    }

    private void OnDisable()
    {
        _player.LevelMoneyChanged -= OnPlayersLevelMoneyChanged;
    }

    private void OnPlayersLevelMoneyChanged(int newAmount)
    {
        _moneyContainer.text = newAmount.ToString();
    }
}
