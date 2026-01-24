using System;

public interface IMinigame
{
    public void StartMinigame();
    public event Action<bool> OnCompletedCorrectly;

}