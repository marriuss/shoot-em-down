using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class Authorizer : MonoBehaviour
{
    [SerializeField] private PlayerDataStorage _playerSavedData;

    public void Authorize()
    {
        PlayerAccount.Authorize(onSuccessCallback: OnPlayerAuthorized);
    }

    private void OnPlayerAuthorized()
    {
        _playerSavedData.LoadData();
    }
}
