using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleUseButton : WorkButton
{
    protected override void OnButtonClick()
    {
        SetInteractable(false);  
    }
}
