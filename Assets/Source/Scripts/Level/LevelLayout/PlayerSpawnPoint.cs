using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : ResetableMonoBehaviour
{
    [SerializeField] private Player _player;

    public override void SetStartState()
    {
        _player.SpawnWeapon(transform.position);
    }
}
