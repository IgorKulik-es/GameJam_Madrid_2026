using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DoorMiniGame
{
    public class RampDoorMiniGame : MonoBehaviour, IMinigame
    {
        [SerializeField] private GameObject ramp;
        [SerializeField] private float rampYEndPosition = 1f;
        [SerializeField] private GameObject line;
        
        private Tween _tween;
        private bool _isActive;
        
        public event Action<bool> OnCompletedCorrectly;

        public void StartMinigame()
        {
            _isActive = true;
            gameObject.SetActive(true);
            _tween = ramp.transform.DOLocalMoveY(rampYEndPosition, 1.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }

        private void Update()
        {
            if (_isActive == false) return;
            
            
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                _tween.Kill();
                bool isCorrect = ramp.transform.position.y < line.transform.position.y;
                OnCompletedCorrectly?.Invoke(isCorrect);
                DOVirtual.DelayedCall(1, () => { gameObject.SetActive(false); });
            }  
        }

      
    }
}
