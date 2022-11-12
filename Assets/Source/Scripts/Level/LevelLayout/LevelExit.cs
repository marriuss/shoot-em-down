using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class LevelExit : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasRectTransform;

    public UnityAction PlayerExitLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Weapon _))
            PlayerExitLevel?.Invoke();
    }

    public void SetSize(float width, float height)
    {
        transform.localScale = new Vector3(width, height, 1);
        _canvasRectTransform.sizeDelta = new Vector2(width, height);
        _canvasRectTransform.SetParent(transform);
    }
}
