using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Slider))]
public abstract class BarView : MonoBehaviour
{
    private Slider _slider;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.interactable = false;
    }

    protected void Enable()
    {
        _canvasGroup.alpha = 1;
    }

    protected void Disable()
    {
        _canvasGroup.alpha = 0;
    }

    protected void ChangeValue(float newValue)
    {
        _slider.value = newValue;
    }

    protected void SetSlider(float minValue, float maxValue)
    {
        _slider.minValue = minValue;
        _slider.maxValue = maxValue;
    }
}
