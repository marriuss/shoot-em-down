using UnityEngine;
using UnityEngine.Events;

public class Head : MonoBehaviour
{
    public UnityAction WasShot;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Bullet _))
            WasShot?.Invoke();
    }
}
