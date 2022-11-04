using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    [SerializeField] private LevelLayoutGenerator _levelLayoutGenerator;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private Player _player;

    private void Start()
    {
        _player.SpawnWeapon(_playerSpawnPoint.position);
    }

}
