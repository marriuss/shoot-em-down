using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LeaderboardEntryView : MonoBehaviour
{
    [SerializeField] private TMP_Text _anonymousContainer;
    [SerializeField] private TMP_Text _rankContainer;
    [SerializeField] private TMP_Text _playerContainer;
    [SerializeField] private TMP_Text _scoreContainer;

    private const string UnknownString = "???";

    private Image _image;
    private string _playerName;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _rankContainer.text = UnknownString;
        _playerName = UnknownString;
        _scoreContainer.text = UnknownString;
    }

    private void Update()
    {
        bool anonymous = string.IsNullOrEmpty(_playerName);
        _playerContainer.text = _playerName;
        _playerContainer.enabled = !anonymous;
        _anonymousContainer.enabled = anonymous;
    }

    public void SetData(LeaderboardEntry leaderboardEntry)
    {
        if (leaderboardEntry == null)
            return;

        _rankContainer.text = leaderboardEntry.Rank;
        _playerName = leaderboardEntry.PlayerName;
        _scoreContainer.text = leaderboardEntry.Score;
    }

    public void SetColor(Color color)
    {
        _image.color = color;
    }
}
