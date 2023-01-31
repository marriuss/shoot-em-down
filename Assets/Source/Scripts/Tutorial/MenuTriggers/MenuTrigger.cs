using UnityEngine;
using UnityEngine.Events;
using Lean.Localization;

public class MenuTrigger : MonoBehaviour
{
    [SerializeField] private LeanPhrase _phrase;

    public event UnityAction<LeanPhrase> Triggered;

    protected void InvokeTrigger()
    {
        Triggered?.Invoke(_phrase);
        gameObject.SetActive(false);
    }
}
