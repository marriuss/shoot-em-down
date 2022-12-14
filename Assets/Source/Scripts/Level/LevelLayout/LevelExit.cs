using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class LevelExit : MonoBehaviour
{
    [SerializeField] private bool _playerExitedLevel;
    [SerializeField] private RectTransform _canvasRectTransform;
    [SerializeField] private Image _image;

    public UnityAction PlayerExitLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Weapon _) && _playerExitedLevel == false)
        {
            PlayerExitLevel?.Invoke();
            _playerExitedLevel = true;
        }
    }

    public void ResetState()
    {
        _playerExitedLevel = false;
    }

    public void SetSize(float width, float height)
    {
        transform.localScale = new Vector3(width, height, 1);
        _canvasRectTransform.sizeDelta = new Vector2(width, height);
        _canvasRectTransform.SetParent(transform);
    }

    public void SetMaterial(Material material)
    {
        _image.material = material;
    }
}
