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
            body.transform.DOLocalMoveY(0.25f, 0.5f).SetRelative().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            doors[0].transform.DOLocalMoveY(0.25f, 0.5f).SetRelative().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            doors[1].transform.DOLocalMoveY(0.25f, 0.5f).SetRelative().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            
            //make do loop
            wheels[0].transform.DOLocalMoveY(0.05f, 0.5f).SetRelative().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            wheels[1].transform.DOLocalMoveY(0.05f, 0.5f).SetRelative().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            
            
            wheels[0].transform.DORotate(new Vector3(0, 0, -60), 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
            wheels[1].transform.DORotate(new Vector3(0, 0, -60), 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
            
        }

        public void PlayOpenCloseAnimation(bool isOpen)
        {
            audioManager.OneShot(AudioEffects.DOOR);
            doors[0].transform.DOMoveX(isOpen? leftDoorOpenPos : leftDoorClosePos, 0.5f).SetRelative().SetEase(Ease.OutSine);
            doors[1].transform.DOMoveX(isOpen? rightDoorOpenPos : rightDoorClosePos, 0.5f).SetRelative().SetEase(Ease.OutSine); 
        }
        
        public void StopAnimation()
        {
            body.transform.DOKill();
            doors[0].transform.DOKill();
            doors[1].transform.DOKill();
            wheels[0].transform.DOKill();
            wheels[1].transform.DOKill();
        }
    }
}
