using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BusMovingMiniGame
{
    public class BusController : MonoBehaviour, IMinigame
    {
        [SerializeField] private float speed = 2.5f;

        [SerializeField] private float accelerationSpeed = 0.05f;
        [SerializeField] private float decelerationSpeed = 0.05f;
        [SerializeField] private float maxAcceleration = 1000f;
        [SerializeField] private float minDeceleration = 0f;
    
        [SerializeField] private BackgroundMover backgroundMover;
        [SerializeField] private StoppingZone stoppingZone;
        [SerializeField] private BusAnimator busAnimator;
    
        public event Action<bool> OnCompletedCorrectly;
    
        private float _multiplier;
        private bool _isActive;

        public enum BackgroundState
        {
            Moving,
            Accelerating,
            Decelerating,
            Stopped
        }
    
        private BackgroundState _currentState = BackgroundState.Stopped;

        private void Update()
        {
            if (_isActive == false) return;
        
            // Replaced Legacy Input with New Input System
            if (Keyboard.current != null)
            {
                if (Keyboard.current.bKey.isPressed)
                {
                    UpdateStatement(BackgroundState.Accelerating);
                }

                if (Keyboard.current.vKey.isPressed)
                {
                    UpdateStatement(BackgroundState.Decelerating);
                }
            }
        
            var delta = speed * _multiplier * Time.deltaTime;
        
            backgroundMover.ExternalUpdate(delta);
            stoppingZone.ExternalUpdate(delta);

            if (delta == 0 && _currentState != BackgroundState.Stopped)
            {
                bool isCorrectStopping = stoppingZone.CheckIfPlayerIsInStoppingZone(transform.position.x);
                OnCompletedCorrectly?.Invoke(isCorrectStopping);
                busAnimator.StopAnimation();
                _isActive = false;
            }
        }
        

        private void UpdateStatement(BackgroundState newState)
        {
            _currentState = newState;
            switch (_currentState)
            {
                case BackgroundState.Moving:
                    _multiplier = 1;
                    break;
                case BackgroundState.Accelerating:
                    _multiplier += accelerationSpeed;
                    _multiplier = Mathf.Min(maxAcceleration, _multiplier);
                    break;
                case BackgroundState.Decelerating:
                    _multiplier -= decelerationSpeed;
                    _multiplier = Mathf.Max(minDeceleration, _multiplier);
                    break;
            }
        }

        public void StartMinigame()
        {
            _isActive = true;
            UpdateStatement(BackgroundState.Moving);
        }

        public BusAnimator GetBusAnimator() => busAnimator;

    }
}
