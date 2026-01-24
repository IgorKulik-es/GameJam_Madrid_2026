using System;
using BusMovingMiniGame;
using DG.Tweening;
using OpenDoreMiniGame;
using PassengerMiniGame;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpellChecker spellChecker;
    [SerializeField] private BusController busController;
    [SerializeField] private OpenDoorGame openDoorGame;
    [SerializeField] private PassengerGame passengerGame;
    [SerializeField] private UIStatsPanel uiStatsPanel;
    
    
    private bool _isPaused = false;


    private void Start()
    {
       StartBusMovingMiniGame();
       
    }


    private void StartBusMovingMiniGame()
    {
        busController.StartMinigame();
        busController.OnCompletedCorrectly += b =>
        {
            
            
            StartOpenDoorMiniGame();
        };
    }

    private void StartOpenDoorMiniGame()
    {
        openDoorGame.StartMinigame();
        openDoorGame.OnCompletedCorrectly += b =>
        {
            DOVirtual.DelayedCall(1,()=>busController.GetBusAnimator().PlayOpenCloseAnimation(true));
            DOVirtual.DelayedCall(2, StartPassengerMiniGame);
        };
    }


    private void StartPassengerMiniGame()
    {
        passengerGame.StartMinigame();
        passengerGame.OnCompletedCorrectly += b =>
        {
            
        };
    }

    private void Update()
    {

        // if (Keyboard.current.spaceKey.wasPressedThisFrame)
        // {
        //     _isPaused = !_isPaused;
        //     spellChecker.SetEnableChecking(_isPaused);
        //     Debug.Log("IsPaused: " + _isPaused);
        // }
    }


    private void OnInputEnded(bool isCorrect)
    {
        spellChecker.SetEnableChecking(false);
    }

    private void OnEnable()
    {
        //SpellChecker.OnWordEnded += OnInputEnded;
    }

    private void OnDisable()
    {
        //SpellChecker.OnWordEnded -= OnInputEnded;
    }
}