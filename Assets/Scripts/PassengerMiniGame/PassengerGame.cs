using System;
using DG.Tweening;
using DoorMiniGame;
using InputGame;
using SoundMiniGame;
using UnityEngine;

namespace PassengerMiniGame
{
    public class PassengerGame : MonoBehaviour, IMinigame
    {
        [SerializeField] private LineController lineController;
        [SerializeField] private RampDoorMiniGame rampDoorMiniGame;
        [SerializeField] private PlayerInputGame playerInputGame;
        [SerializeField] private SoundGame soundGame;
        
        public event Action<bool> OnCompletedCorrectly;

        public void OnButtonPassengerClicked(int i)
        {
            switch (i)
            {
                case 1 :
                    RunGamyByTheType(PassengerType.VISION);
                    //vision
                    break;
                case 2:
                    RunGamyByTheType(PassengerType.HEARING_BAD);
                    // hearing
                    break;
                case 3:
                    RunGamyByTheType(PassengerType.SOUND_SENSITIVE);
                    // sound sens
                    break;
                case 4:
                    RunGamyByTheType(PassengerType.MOBILITY);
                    // mobility
                    break;
                
            }
        }

        public void StartMinigame()
        {
            lineController.SetEnabled(true);
            
            InitGameLoop();
        }

        private void InitGameLoop()
        {
            PassengerType type = lineController.GetFirstInQueue();
            
        }


        private void RunGamyByTheType(PassengerType type)
        {
            
            switch (type)
            {
                case PassengerType.MOBILITY:
                    rampDoorMiniGame.StartMinigame();
                    rampDoorMiniGame.OnCompletedCorrectly += NextPassenger;
                    break;
                case PassengerType.VISION:
                    playerInputGame.StartMinigame();
                    playerInputGame.OnCompletedCorrectly += NextPassenger;
                    break;
                
                case PassengerType.SOUND_SENSITIVE:
                    soundGame.StartMinigame();
                    soundGame.SetGoal(SoundGame.SoundFor.Low);
                    soundGame.OnCompletedCorrectly += NextPassenger;
                    break;
                
                case PassengerType.HEARING_BAD:
                    soundGame.StartMinigame();
                    soundGame.SetGoal(SoundGame.SoundFor.High);
                    soundGame.OnCompletedCorrectly += NextPassenger;
                    break;
                
                default:
                    DOVirtual.DelayedCall(1, () =>
                    {
                        lineController.PopQueue();
                    });
                   
                    break;
            }
        }

        private void NextPassenger(bool isSuccess)
        {
            DOVirtual.DelayedCall(1, () =>
            {
                lineController.PopQueue();
            });
          
        }
   
    }
}