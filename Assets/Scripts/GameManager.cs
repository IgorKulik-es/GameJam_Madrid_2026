using System;
using BusMovingMiniGame;
using DG.Tweening;
using InputGame;
using OpenDoreMiniGame;
using PassengerMiniGame;
using SoundMiniGame;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpellChecker spellChecker;
    [SerializeField] private BusController busController;
    [SerializeField] private OpenDoorGame openDoorGame;
    [SerializeField] private PassengerGame passengerGame;
    [SerializeField] private UIStatsPanel uiStatsPanel;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private UIPauseMenu uiPauseMenu;
    
    
    public static GameManager Instance { get; private set; }
    
    public int totalTime = 60 * 3;
    
    
    
    private bool _isPaused = false;
    private int _timer; 
    private InputAction _pauseAction;
    
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        _timer = totalTime;
       StartBusMovingMiniGame();
       
       DOVirtual.DelayedCall(1, () =>
       { 
           _timer--;
          uiStatsPanel.UpdateTimerText(_timer);
       }).SetLoops(totalTime);
       
       
       // use input from new Input System for the button ESC to pause the game
       _pauseAction = new InputAction(binding: "<Keyboard>/escape");
       _pauseAction.performed += _ =>
       {
           _isPaused = !_isPaused;
           if (_isPaused)
           {
               uiPauseMenu.OpenPauseMenu();
           }
           else
           {
               uiPauseMenu.ClosePauseMenu();
           }
       };
       _pauseAction.Enable();
    }
    
    public void SetPause(bool pause)
    {
        _isPaused = pause;
    }

    private void OnDestroy()
    {
        _pauseAction?.Dispose();
    }


    private void StartBusMovingMiniGame()
    {
        busController.StartMinigame();
        busController.OnCompletedCorrectly += b =>
        {
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