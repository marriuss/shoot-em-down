using UnityEngine;

public class ExitMenuButton : WorkButton
{
    [SerializeField] private Menu _menu;

    protected override void OnButtonClick()
    {
        _menu.Close();
    }
}