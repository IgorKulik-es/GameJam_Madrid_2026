using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace OpenDoreMiniGame
{
    public class OpenDoorGame : MonoBehaviour
    {
        [SerializeField] private Image[] combinationIcons; 
        [SerializeField] private Sprite[] allIcons;
        [SerializeField] private Image background;
        
        private int[] _combination = {1, 2, 3};
        private int[] _playerCombination = {-1, -1, -1};
        
        private int _currentIconIndex = 0;
        
        public void OnKeyButtonPressed(int key)
        {
            _playerCombination[_currentIconIndex] = key;
            _currentIconIndex++;
            
            if (_currentIconIndex == 3)
            {
                CheckCombination();
            }
        }
        
        
        private void ResetCombination()
        {
            _playerCombination = new int[] {-1, -1, -1};
            _currentIconIndex = 0;
        }

        private void CheckCombination()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_playerCombination[i] != _combination[i])
                {
                    PlayWrongAnimation();
                    return;
                }
            }
            
            PlayCorrectAnimation();
            ResetCombination();
        }
        
        private void PlayCorrectAnimation()
        {
            DOTween.Sequence()
            .Append(background.DOColor(new Color(255,0,0,0.5f), 0.5f))
            .Append(background.DOColor(new Color(0,0,0,0), 0.5f));
            
        }
        
        private void PlayWrongAnimation()
        {
            DOTween.Sequence()
            .Append(background.DOColor(new Color(255,0,0,0.5f), 0.5f))
            .Append(background.DOColor(new Color(0,0,0,0), 0.5f));
        }
        
    }
}
