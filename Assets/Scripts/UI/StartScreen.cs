using UnityEngine.Events;

public class StartScreen : Screen
{
    public UnityAction PlayButtonClick;

    protected override void OnButtonClick()
    {
        PlayButtonClick?.Invoke();
    }
}