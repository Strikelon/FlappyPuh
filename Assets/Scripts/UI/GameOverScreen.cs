using UnityEngine.Events;

public class GameOverScreen : Screen
{
    public UnityAction RestartButtonClick;

    protected override void OnButtonClick()
    {
        RestartButtonClick?.Invoke();
    }
}