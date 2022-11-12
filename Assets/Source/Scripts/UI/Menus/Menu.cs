using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : ResetableMonoBehaviour
{
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
        _canvasGroup.blocksRaycasts = true;
        StartCoroutine(OpenMenu(_openingTime));
    }

    public void Close()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0;
        UnfrozeTime();
    }

    private void FrozeTime()
    {
        Time.timeScale = 0;
    }

    private void UnfrozeTime()
    {
        Time.timeScale = 1;
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

        _canvasGroup.interactable = true;
        FrozeTime();
    }
}