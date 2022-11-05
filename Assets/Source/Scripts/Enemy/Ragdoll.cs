using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Ragdoll : MonoBehaviour
{
    private List<Collider> _colliders = new List<Collider>();

    private void OnEnable()
    {
        ChangeEnability(true);
    }

    private void OnDisable()
    {
        ChangeEnability(false);
    }

    public void Initialize(List<Collider> colliders)
    {
        _colliders = colliders;
    }

    private void ChangeEnability(bool isEnable)
    {
        Rigidbody attachedRigidbody;

        foreach (Collider collider in _colliders)
        {
            collider.enabled = isEnable;
            attachedRigidbody = collider.GetComponent<Rigidbody>();

            if (attachedRigidbody != null)
            {
                attachedRigidbody.useGravity = isEnable;
                attachedRigidbody.isKinematic = !isEnable;
            }
        }
    }
}
