using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputGame
{
    public class PlayerInputGame : MonoBehaviour, IMinigame
    {
        public UIPlayerInput uiPlayerInput;
        public SpellChecker spellChecker;
        // Singleton instance for easy access
        public static PlayerInputGame Instance { get; private set; }

        // Event that other scripts can subscribe to
        public static event Action<char> OnKeyButtonTyped;

        public event Action<bool> OnCompletedCorrectly;


        private bool _isActive;
   
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    
        public void StartMinigame()
        {
            _isActive = true;
            StartListenButtons();
            uiPlayerInput.StartMinigame();
            uiPlayerInput.OnCompletedCorrectly += (isSuccess)=>
            {
                Debug.Log("OnFinishGame True" );
                StopListenButtons();
                OnCompletedCorrectly?.Invoke(isSuccess);
                SpellChecker.OnWordEnded -= OnInputEnded;
            };
            spellChecker.SetEnableChecking(true);
            SpellChecker.OnWordEnded += OnInputEnded;
          
        }
        
        
        private void Update()
        {
            if(_isActive) return;
            
        }


        private void OnInputEnded(bool isCorrect)
        {
            spellChecker.SetEnableChecking(false);
        }

        private void StopListenButtons()
        {
            // Subscribe to the global text input event
            if (Keyboard.current != null)
            {
                Keyboard.current.onTextInput -= HandleTextInput;
            }
        }

        private void StartListenButtons()
        {
                Keyboard.current.onTextInput += HandleTextInput;
        }

        private void HandleTextInput(char character)
        {
            OnKeyButtonTyped?.Invoke(character);
        }

    }
}
