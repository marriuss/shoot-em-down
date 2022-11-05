using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Ragdoll))]
public class Enemy : MonoBehaviour
{
    [SerializeField, Min(1)] private int _maxHealth;
    [SerializeField] private Head _head;
    [SerializeField] private Body _body;

    private Ragdoll _ragdoll;
    private Animator _animator;
    private Rigidbody _rigidbody;

    public UnityAction<Enemy> KnockedOut;

    public int Health { get; private set; }
    public bool IsKnockedOut { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        Collider[] colliders = GetComponentsInChildren<Collider>();
        List<Collider> ragdollColliders = new List<Collider>();

        Collider headCollider = _head.GetComponent<Collider>();
        Collider bodyCollider = _body.GetComponent<Collider>();

        foreach (Collider collider in colliders)
        {
            if (collider != headCollider && collider != bodyCollider)
                ragdollColliders.Add(collider);
        }

        _ragdoll = GetComponent<Ragdoll>();
        _ragdoll.Initialize(ragdollColliders);
    }

    private void OnEnable()
    {
        _head.WasShot += OnHeadShot;
        _body.WasShot += OnBodyShot;
    }

    private void OnDisable()
    {
        _head.WasShot -= OnHeadShot;
        _body.WasShot -= OnBodyShot;
    }

    private void Start()
    {
        SwitchPhysics(false);
        _head.Initialize(this);
        _body.Initialize(this);
        Health = _maxHealth;
    }

    private void TakeDamage()
    {
        Health--;

        if (Health <= 0)
            KnockOut();
    }

    private void OnHeadShot()
    {
        KnockOut();
    }

    private void OnBodyShot()
    {
        TakeDamage();
    }

    private void KnockOut()
    {
        IsKnockedOut = true;
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
