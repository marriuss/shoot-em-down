using UnityEngine;

public class MenuTrigger : MonoBehaviour
{
    [SerializeField] private Menu _menu;

    private bool _triggered;

    private void Start()
    {
        _triggered = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Weapon _) && _triggered == false)
        {
            _triggered = true;
            _menu.Open();
        }
    }
}
