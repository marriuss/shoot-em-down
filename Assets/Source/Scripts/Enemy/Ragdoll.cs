using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Ragdoll : ResetableMonoBehaviour
{
    [SerializeField] private Transform _baseBone;
    private List<EnemyPart> _enemyParts = new List<EnemyPart>();

    private void OnEnable()
    {
        ChangeEnability(true);
    }

    private void OnDisable()
    {
        ChangeEnability(false);
    }

    public void Initialize(List<EnemyPart> enemyParts)
    {
        _enemyParts = enemyParts;
    }

    public override void SetStartState()
    {
        _baseBone.localPosition = Vector3.zero;
    }

    private void ChangeEnability(bool isEnable)
    {
        Rigidbody attachedRigidbody;

        foreach (EnemyPart enemyPart in _enemyParts)
        {
            attachedRigidbody = enemyPart.Rigidbody;
            attachedRigidbody.useGravity = isEnable;
            attachedRigidbody.isKinematic = !isEnable;
        }
    }
}
