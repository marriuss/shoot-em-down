using UnityEngine;

public class OpenMenuButton : WorkButton
{
    [SerializeField] private Menu _menu;

    protected override void OnButtonClick()
    {
        _menu.Open();
    }
}