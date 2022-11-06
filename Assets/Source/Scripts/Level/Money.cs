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

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;

        if (collider.TryGetComponent(out Weapon _) || collider.TryGetComponent(out Bullet _))
            Destroy(gameObject);
    }
}
