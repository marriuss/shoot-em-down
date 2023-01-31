using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private MenuGroup _menuGroup;
    [SerializeField] private PauseMenuView _view;
    
    private void OnApplicationFocus(bool focus)
    {
        if (_menuGroup.MenusNotOpened && (focus == false))
            _menuGroup.Open(_view);
    }
}
