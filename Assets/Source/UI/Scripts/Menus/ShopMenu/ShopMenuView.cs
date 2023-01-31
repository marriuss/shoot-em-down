using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuView : MenuView
{
    [SerializeField] private TMP_Text _textContainer;
    [SerializeField] private Button _exitButton;

    public void SetMoney(int money)
    {
        _textContainer.text = money.ToString();
    }

    protected override void SetActive(bool active)
    {
        _exitButton.gameObject.SetActive(active);
        gameObject.SetActive(active);
    }
}
