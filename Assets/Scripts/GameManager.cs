using BusMovingMiniGame;
using DG.Tweening;
using InputGame;
using OpenDoreMiniGame;
using PassengerMiniGame;
using SoundMiniGame;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpellChecker spellChecker;
    [SerializeField] private BusController busController;
    [SerializeField] private OpenDoorGame openDoorGame;
    [SerializeField] private PassengerGame passengerGame;
    [SerializeField] private UIStatsPanel uiStatsPanel;
    [SerializeField] private AudioManager audioManager;
    
    
    public int totalTime = 60 * 3;
    
    private bool _isPaused = false;

    
    private int _timer; 


    private void Start()
    {
        _timer = totalTime;
       StartBusMovingMiniGame();
       
       DOVirtual.DelayedCall(1, () =>
       { 
           _timer--;
          uiStatsPanel.UpdateTimerText(_timer);
       }).SetLoops(totalTime);
    }


    private void StartBusMovingMiniGame()
    {
        busController.StartMinigame();
        busController.OnCompletedCorrectly += b =>
        {
            PlaySoundGameSuccess(b);
            busController.GetBusAnimator().PlayOpenCloseAnimation(true);
            DOVirtual.DelayedCall(1, StartPassengerMiniGame);
        };
    }

    private void StartOpenDoorMiniGame()
    {
        openDoorGame.StartMinigame();
        openDoorGame.OnCompletedCorrectly += b =>
        {
            PlaySoundGameSuccess(b);
            DOVirtual.DelayedCall(1,()=>busController.GetBusAnimator().PlayOpenCloseAnimation(true));
            DOVirtual.DelayedCall(2, StartPassengerMiniGame);
        };
    }

    private void StartPassengerMiniGame()
    {
        passengerGame.StartMinigame();
        passengerGame.OnCompletedCorrectly += b =>
        {
            PlaySoundGameSuccess(b);
            DOVirtual.DelayedCall(1, () => busController.GetBusAnimator().PlayOpenCloseAnimation(false));
            DOVirtual.DelayedCall(2, StartBusMovingMiniGame);
        };
    }

    private void PlaySoundGameSuccess(bool result)
    {
        if (result)
            audioManager.OneShot(AudioEffects.SUCCESS);
        else
            audioManager.OneShot(AudioEffects.FAILURE);
    }
    
}