using UnityEngine;
using TMPro;

public class LeaderboardEntryView : MonoBehaviour
{
    [SerializeField] private TMP_Text _rankContainer;
    [SerializeField] private TMP_Text _playerContainer;
    [SerializeField] private TMP_Text _scoreContainer;

    private const string UnknownString = "???";

    private void Awake()
    {
        _rankContainer.text = UnknownString;
        _playerContainer.text = UnknownString;
        _scoreContainer.text = UnknownString;
    }

    public void SetData(LeaderboardEntry leaderboardEntry)
    {
        _rankContainer.text = leaderboardEntry.Rank;
        _playerContainer.text = leaderboardEntry.PlayerName;
        _scoreContainer.text = leaderboardEntry.Score;
    }
}
