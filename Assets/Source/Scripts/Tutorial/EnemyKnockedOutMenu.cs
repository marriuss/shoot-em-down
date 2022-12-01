using UnityEngine;

public class EnemyKnockedOutMenu : Menu
{
    [SerializeField] private Player _player;

    private bool _enemyKnockedOut;

    private void OnEnable()
    {
        _player.ShotCollider += OnPlayerShotCollider;
    }

    private void OnDisable()
    {
        _player.ShotCollider -= OnPlayerShotCollider;
    }

    public override void SetStartState()
    {
        _enemyKnockedOut = false;
    }

    private void OnPlayerShotCollider(Collider collider)
    {
        if (collider.TryGetComponent(out EnemyPart _) && _enemyKnockedOut == false)
        {
            _enemyKnockedOut = true;
            Open();
        }
    }
}
