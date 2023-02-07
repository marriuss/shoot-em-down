using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _moneyContainer;

    private void Update()
    {
        _moneyContainer.text = _player.LevelMoney.ToString();
    }
}
