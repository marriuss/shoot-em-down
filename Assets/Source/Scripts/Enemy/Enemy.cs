using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Ragdoll))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour
{
    [SerializeField, Min(1)] private int _maxHealth;
    [SerializeField] private UnityEvent _playerDetected;
    [SerializeField] private UnityEvent _playerUndetected;
    [SerializeField] private float _maxKnockedOutTime;

    private Head _head;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Ragdoll _ragdoll;
    private List<EnemyPart> _enemyParts;
    private CapsuleCollider _triggerCollider;

    public Health Health { get; private set; }
    public bool IsKnockedOut { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        _animator = GetComponent<Animator>();
        _ragdoll = GetComponent<Ragdoll>();
        _enemyParts = GetComponentsInChildren<EnemyPart>().ToList();

        foreach (EnemyPart enemyPart in _enemyParts)
        {
            Head head = enemyPart as Head;

            if (head != null)
                _head = head;
            
            enemyPart.Initialize(this);
        }

        _ragdoll.Initialize(_enemyParts);
        SwitchPhysics(false);
        Health = new Health(_maxHealth);

        _triggerCollider = GetComponent<CapsuleCollider>();
        _triggerCollider.isTrigger = true;
    }

    private void OnEnable()
    {
        foreach (EnemyPart enemyPart in _enemyParts)
            enemyPart.WasShot += OnEnemyPartWasShot;
    }

    private void OnDisable()
    {
        foreach (EnemyPart enemyPart in _enemyParts)
            enemyPart.WasShot -= OnEnemyPartWasShot;
    }

    public void Translate(Vector3 position)
    {
        transform.position = position;
    }

    public void ResetState()
    {
        foreach (EnemyPart enemyPart in _enemyParts)
            enemyPart.enabled = true;

        SwitchPhysics(false);
        Health.Heal();
        IsKnockedOut = false;
        _ragdoll.ResetState();
    }

    private void TakeDamage()
    {
        Health.ApplyDamage(1);

        if (Health.CurrentValue <= 0)
            KnockOut();
    }

    private void OnEnemyPartWasShot(EnemyPart enemyPart)
    {
        if (enemyPart == _head)
        {
            KnockOut();
        }
        else
        {
            TakeDamage();
        }
    }

    private void KnockOut()
    {
        IsKnockedOut = true;
        SwitchPhysics(true);

        foreach (EnemyPart enemyPart in _enemyParts)
            enemyPart.enabled = false;
    }

    private void SwitchPhysics(bool isRagdollEnabled)
    {
        _rigidbody.useGravity = !isRagdollEnabled;
        _rigidbody.isKinematic = isRagdollEnabled;
        _ragdoll.enabled = isRagdollEnabled;
        _animator.enabled = !isRagdollEnabled;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Weapon _))
            _playerDetected?.Invoke();
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Weapon _))
            _playerUndetected?.Invoke();
    }
}
