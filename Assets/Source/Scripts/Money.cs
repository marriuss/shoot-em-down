using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private float _rotationAnglePerSecond;

    public int Value => _value;

    private void Update()
    {
        transform.Rotate(_rotationAnglePerSecond * Time.deltaTime * Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Weapon _) || other.TryGetComponent(out Bullet _))
            Destroy(gameObject);
    }
}
