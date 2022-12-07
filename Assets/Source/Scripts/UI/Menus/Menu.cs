using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : ResetableMonoBehaviour
{
    [SerializeField] private UnityEvent _opened;
    [SerializeField] private UnityEvent _closed;
    [SerializeField, Min(0.0f)] private float _openingTime;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void SetStartState()
    {
        Close();
    }

    public void Open()
    {
        ApplicationPause.OpenMenu(this);
        _canvasGroup.blocksRaycasts = true;
        StartCoroutine(OpenMenu(_openingTime));
    }

    public void Close()
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0;
        TimeChanger.UnfrozeTime();
        ApplicationPause.CloseMenu(this);
        _closed?.Invoke();
    }

    private IEnumerator OpenMenu(float openingTime)
    {
        if (openingTime > 0)
        {
            float timeElapsed = 0.0f;

            while (_canvasGroup.alpha < 1)
            {
                timeElapsed += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(0, 1, timeElapsed / openingTime);
                yield return null;
            }
        }
        else
        {
            _canvasGroup.alpha = 1;
        }

        TimeChanger.FrozeTime();
        _canvasGroup.interactable = true;
        _opened?.Invoke();
    }
}