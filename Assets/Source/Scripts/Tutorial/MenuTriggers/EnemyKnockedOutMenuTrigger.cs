using UnityEngine;

public class EnemyKnockedOutMenuTrigger : MenuTrigger
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.ShotCollider += OnPlayerShotCollider;
    }

    private void OnDisable()
    {
        _player.ShotCollider -= OnPlayerShotCollider;
    }

    private void OnPlayerShotCollider(Collider collider)
    {
        if (collider.TryGetComponent(out EnemyPart _))
            InvokeTrigger();
    }
}
