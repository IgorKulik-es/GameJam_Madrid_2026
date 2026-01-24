using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DoorMiniGame
{
    public class DoorMiniGame : MonoBehaviour
    {
        [SerializeField] private GameObject ramp;
        [SerializeField] private float rampYEndPosition = 1f;
        [SerializeField] private GameObject line;
        
        private Tween _tween;

        private void Start()
        { 
            StartMinigame();
        }


        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                _tween.Kill();
                if (ramp.transform.position.y < line.transform.position.y)
                {
                    Debug.Log("Win");
                }
                else
                {
                    Debug.Log("Lose");
                }
            }  
        }

        private void StartMinigame()
        {
            _tween = ramp.transform.DOMoveY(rampYEndPosition, 2).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
