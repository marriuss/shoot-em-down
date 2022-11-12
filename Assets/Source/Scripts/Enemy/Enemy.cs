using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Ragdoll))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : ResetableMonoBehaviour
{
    [SerializeField, Min(1)] private int _maxHealth;

    private Head _head;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Ragdoll _ragdoll;
    private List<EnemyPart> _enemyParts;

    public Health Health { get; private set; }
    public bool IsKnockedOut { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;

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

    public override void SetStartState()
    {
        SwitchPhysics(false);
        Health.Heal();
        IsKnockedOut = false;
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
    }

    private void SwitchPhysics(bool isRagdollEnabled)
    {
        _ragdoll.enabled = isRagdollEnabled;
        _animator.enabled = !isRagdollEnabled;
    }
}
