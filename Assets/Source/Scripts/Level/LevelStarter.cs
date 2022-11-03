using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private Weapon _player;

    private void Start()
    {
        _player.Spawn(_playerSpawnPoint.position);
    }

}
