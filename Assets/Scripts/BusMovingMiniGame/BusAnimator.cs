using System;
using DG.Tweening;
using UnityEngine;

namespace BusMovingMiniGame
{
    public class BusAnimator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer body;
        [SerializeField] private SpriteRenderer[] wheels;
        [SerializeField] private SpriteRenderer[] doors;
        [SerializeField] private AudioManager audioManager;
        
        public float leftDoorOpenPos = -0.44f;
        public float leftDoorClosePos = -0.04f;

        public float rightDoorOpenPos = 0.46f;
        public float rightDoorClosePos = 0.06f;
        

        private void Start()
        {
            body.transform.DOLocalMoveY(0.5f, 0.5f).SetRelative().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }

        public void PlayOpenCloseAnimation(bool isOpen)
        {
            audioManager.OneShot(AudioEffects.DOOR);
            
            doors[0].transform.DOMoveX(isOpen? leftDoorOpenPos : leftDoorClosePos, 0.5f).SetRelative().SetEase(Ease.OutSine);
            doors[1].transform.DOMoveX(isOpen? rightDoorOpenPos : rightDoorClosePos, 0.5f).SetRelative().SetEase(Ease.OutSine); 
        }
    }
}
