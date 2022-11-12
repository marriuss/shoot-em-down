public class StartNewLevelButton : WorkButton
{
    protected override void OnButtonClick()
    {
        LevelStarter.StartNewLevel();
    }
}
