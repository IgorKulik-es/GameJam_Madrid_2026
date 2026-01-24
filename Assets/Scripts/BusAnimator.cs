using DG.Tweening;
using UnityEngine;

public class BusAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private SpriteRenderer[] wheels;


    private void Start()
    {
        body.transform.DOLocalMoveY(0.5f, 0.5f).SetRelative().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        
    }


}
