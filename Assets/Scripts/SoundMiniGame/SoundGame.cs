using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SoundMiniGame
{
    public class SoundGame : MonoBehaviour, IMinigame
    {
        [SerializeField] private Slider slider;
        [SerializeField] private AudioManager audioManager;
        public float minSuccessValue = 0.25f;
        public float maxSuccessValue = 0.75f;
        
        
        public event Action<bool> OnCompletedCorrectly;
        
        public enum SoundFor
        {
            Low,
            High
        }
        
        private SoundFor _currentGoal;
        
        public void OnButtonClicked()
        {
           var value = slider.value;
           Debug.Log($"Slider value: {value}");
           
           if (_currentGoal == SoundFor.Low)
           {
               OnCompletedCorrectly?.Invoke(value < minSuccessValue);
           }
           else
           {
               OnCompletedCorrectly?.Invoke(value > maxSuccessValue);
           }
           audioManager.OneShot(AudioEffects.ANNOUNCEMENT);
           DOVirtual.DelayedCall(1, () => gameObject.SetActive(false));
        }

        public void StartMinigame()
        {
            slider.value = 0.5f;
            gameObject.SetActive(true);
        }
        
        public void SetGoal(SoundFor soundFor)
        {
            _currentGoal = soundFor;
        }


    }
}