using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Money : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private float _rotationAnglePerSecond;
    [SerializeField] private UnityEvent<int> _playerCollectedMoney;

    private const float DestructionDelay = 0.5f;

    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        transform.Rotate(_rotationAnglePerSecond * Time.deltaTime * Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Weapon _) || other.TryGetComponent(out Bullet _))
        {
            _playerCollectedMoney?.Invoke(_value);
            _boxCollider.enabled = false;
        }
        Destroy(gameObject, DestructionDelay);
    }
}
