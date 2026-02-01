using UnityEngine;

namespace BusMovingMiniGame
{
    public class BackgroundMover : MonoBehaviour
    {
        [SerializeField] private GameObject[] backgrounds;
        [SerializeField] private Vector2 boundaries;
        [SerializeField] private Vector2 resetPosition;


        public void ExternalUpdate(float delta)
        {
            foreach (var bg in backgrounds)
            {
                var pos = bg.transform.localPosition;
                pos.x -= delta;
                if (pos.x <= boundaries.x)
                {
                    pos.x = resetPosition.x;
                    pos.y = resetPosition.y;
                }
            
                bg.transform.localPosition = pos;
            }
        }
    
    }
}
