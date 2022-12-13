using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class MenuGroup : MonoBehaviour
{
    [SerializeField] private Menu _firstMenu;

    private Stack<Menu> _openedMenus;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _openedMenus = new Stack<Menu>();
        _canvasGroup = GetComponent<CanvasGroup>();

        if (_firstMenu != null)
            Open(_firstMenu);
    }

    public void Open(Menu menu)
    {
        if (_openedMenus.Count == 0)
        {
            ChangeAppearance(true);
        }
        else
        {
            _openedMenus.Peek().Disappear();
        }

        menu.Appear();
        _openedMenus.Push(menu);
    }

    public void Close(Menu menu)
    {
        Menu lastOpenedMenu = _openedMenus.Pop();

        if (menu != lastOpenedMenu)
            return;

        menu.Disappear();

        if (_openedMenus.Count == 0)
        {
            ChangeAppearance(false);
        }
        else
        {
            _openedMenus.Peek().Appear();
        }
    }

    public void CloseMenus()
    {
        Menu menu;

        while (_openedMenus.Count > 0)
        {
            menu = _openedMenus.Pop();
            menu.Disappear();
        }

        ChangeAppearance(false);
    }

    private void ChangeAppearance(bool isVisible)
    {
        float alpha = isVisible ? 1 : 0;
        _canvasGroup.alpha = alpha;
        _canvasGroup.blocksRaycasts = isVisible;
        _canvasGroup.interactable = isVisible;
    }
}
