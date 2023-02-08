using Agava.YandexGames;
using Lean.Localization;

public class LeaderboardEntry
{
    public string Rank { get; private set; }
    public string PlayerName { get; private set; }
    public string Score { get; private set; }

    public LeaderboardEntry(LeaderboardEntryResponse entry)
    {
        if (entry == null)
            return;

        Rank = entry.rank.ToString();
        PlayerName = entry.player.publicName;
        Score = entry.score.ToString();
    }
}
