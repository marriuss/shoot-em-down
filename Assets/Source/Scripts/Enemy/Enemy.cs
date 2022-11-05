using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Ragdoll))]
public class Enemy : MonoBehaviour
{
    [SerializeField, Min(1)] private int _health;
    [SerializeField] private Head _head;
    [SerializeField] private Body _body;

    private Ragdoll _ragdoll;
    private Animator _animator;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        Collider[] colliders = GetComponentsInChildren<Collider>();
        List<Collider> ragdollColliders = new List<Collider>();

        foreach (Collider collider in colliders)
        {
            if (collider == _head.GetComponent<Collider>() || collider == _body.GetComponent<Collider>())
                continue;

            ragdollColliders.Add(collider);
        }

        _ragdoll = GetComponent<Ragdoll>();
        _ragdoll.Initialize(ragdollColliders);
    }

    private void OnEnable()
    {
        _head.WasShot += OnHeadShot;
    }

    private void OnDisable()
    {
        _head.WasShot -= OnHeadShot;
    }

    private void Start()
    {
        SwitchPhysics(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Bullet bullet))
            TakeDamage(bullet.Damage);
    }

    private void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health < 0)
            KnockOut();
    }

    private void OnHeadShot()
    {
        KnockOut();
    }

    private void KnockOut()
    {
        SwitchPhysics(true);
    }

    private void SwitchPhysics(bool isRagdollEnabled)
    {
        _ragdoll.enabled = isRagdollEnabled;
        _animator.enabled = !isRagdollEnabled;
        _head.enabled = !isRagdollEnabled;
        _body.enabled = !isRagdollEnabled;
        _rigidbody.isKinematic = isRagdollEnabled;
        _rigidbody.useGravity = !isRagdollEnabled;
    }
}
