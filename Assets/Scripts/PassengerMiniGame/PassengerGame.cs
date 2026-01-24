using System;
using DoorMiniGame;
using UnityEngine;

namespace PassengerMiniGame
{
    public class PassengerGame : MonoBehaviour, IMinigame
    {
        [SerializeField] private LineController lineController;
        [SerializeField] private RampDoorMiniGame rampDoorMiniGame;

        public event Action<bool> OnCompletedCorrectly;

        public void StartMinigame()
        {
            lineController.SetEnabled(true);
            
            InitGameLoop();
        }

        private void InitGameLoop()
        {
            PassengerType type = lineController.GetFirstInQueue();
            
            if (type != PassengerType.NONE)
            {
                RunGamyByTheType(type);
            }
            
        }


        private void RunGamyByTheType(PassengerType type)
        {
            
            switch (type)
            {
                case PassengerType.MOBILITY:
                    rampDoorMiniGame.StartMinigame();
                    rampDoorMiniGame.OnCompletedCorrectly += (isSuccess) =>
                    {
                        if (isSuccess)
                        {
                              
                        }
                        
                        lineController.PopQueue(); 
                        InitGameLoop();
                    };
                    break;
                default:
                    lineController.PopQueue();
                    InitGameLoop();
                    break;
            }
            
            
        }
   
    }
}