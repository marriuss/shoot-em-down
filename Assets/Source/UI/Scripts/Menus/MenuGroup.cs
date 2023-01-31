using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class MenuGroup : MonoBehaviour
{
    private Stack<MenuView> _activeMenuViews;
    private CanvasGroup _canvasGroup;

    public bool MenusNotOpened => _activeMenuViews.Count == 0;

    private void Awake()
    {
        _activeMenuViews = new Stack<MenuView>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        ChangeAppearance(false);
    }

    public void Open(MenuView view)
    {
        if (MenusNotOpened)
        {
            ChangeAppearance(true);
        }
        else
        {
            _activeMenuViews.Peek().Disappear();
        }

        view.Appear();
        _activeMenuViews.Push(view);
    }

    public void Close(MenuView view)
    {
        MenuView lastOpenedView = _activeMenuViews.Pop();

        if (view != lastOpenedView)
            return;

        view.Disappear();

        if (MenusNotOpened)
        {
            ChangeAppearance(false);
        }
        else
        {
            _activeMenuViews.Peek().Appear();
        }
    }

    public void CloseMenus()
    {
        MenuView view;

        while (MenusNotOpened == false)
        {
            view = _activeMenuViews.Pop();
            view.Disappear();
        }

        ChangeAppearance(false);
    }

    public void CloseLastOpenedMenu()
    {
        if (MenusNotOpened)
            return;

        MenuView lastOpenedView = _activeMenuViews.Peek();
        Close(lastOpenedView);
    }

    private void ChangeAppearance(bool isVisible)
    {
        if (isVisible)
        {
            TimeChanger.FrozeTime();
        }
        else
        {
            TimeChanger.UnfrozeTime();
        }

        _canvasGroup.alpha = isVisible ? 1 : 0;
        _canvasGroup.blocksRaycasts = isVisible;
        _canvasGroup.interactable = isVisible;
    }
}