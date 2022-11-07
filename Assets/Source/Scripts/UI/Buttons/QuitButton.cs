using UnityEngine;

public class QuitButton : WorkButton
{
    protected override void OnButtonClick()
    {
        Application.Quit();
    }
}