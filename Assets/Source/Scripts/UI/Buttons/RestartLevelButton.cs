public class RestartLevelButton : WorkButton
{
    protected override void OnButtonClick()
    {
        LevelStarter.RestartLevel();
    }
}