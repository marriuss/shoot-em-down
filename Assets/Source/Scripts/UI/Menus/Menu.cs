using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
    [SerializeField] private UnityEvent _opened;
    [SerializeField] private UnityEvent _closed;

    private MenuGroup _menuGroup;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _menuGroup = GetComponentInParent<MenuGroup>();
    }

    public void Open()
    {
        if (_menuGroup != null)
            _menuGroup.Open(this);
        else
            Appear();
    }

    public void Close()
    {
        if (_menuGroup != null)
            _menuGroup.Close(this);
        else
            Disappear();
    }

    public void Appear()
    {
        TimeChanger.FrozeTime();
        ChangeAppearance(true);
        ApplicationPause.OpenMenu(this);
        _opened?.Invoke();
    }

    public void Disappear()
    {
        ChangeAppearance(false);
        TimeChanger.UnfrozeTime();
        ApplicationPause.CloseMenu(this);
        _closed?.Invoke();
    }

    private void ChangeAppearance(bool isVisible)
    {
        float alpha = isVisible ? 1 : 0;
        _canvasGroup.alpha = alpha;
        _canvasGroup.blocksRaycasts = isVisible;
        _canvasGroup.interactable = isVisible;
    }
}