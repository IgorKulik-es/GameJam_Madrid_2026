using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace InputGame
{
    public class UIPlayerInput : MonoBehaviour,IMinigame
    {
    
        [SerializeField] private TMP_Text inputText;
        [SerializeField] private GameObject inputPanel;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private GameObject[] panels;
        [SerializeField] private Image button;
        [SerializeField] private SpellChecker _spellChecker;
    
        private Color _defaultColor;
        private Image _image;
        
        public event Action<bool> OnCompletedCorrectly;

        public void StartMinigame()
        {
            int i = Random.Range(0, panels.Length);
            _spellChecker.SetCorrectWord(panels[i].GetComponentInChildren<Text>().text);
            button.color = panels[i].GetComponentInChildren<Image>().color;
           gameObject.SetActive(true);
        }
        
        
        private void Start()
        {
            inputText.text = "____";
            _defaultColor = inputPanel.GetComponent<Image>().color;
            _image = inputPanel.GetComponent<Image>();
        }

        public void SetInputText(string text)
        {
            inputText.text = text;
        }

        private void OnWordEntered(bool isCorrect)
        {
            if (isCorrect)
            {
                PlayCorrectAnimation();
                audioManager.OneShot(AudioEffects.SUCCESS);
            }
            else
            {
                PlayWrongAnimation();
                audioManager.OneShot(AudioEffects.FAILURE);
            }
            
            OnCompletedCorrectly?.Invoke(isCorrect);
            DOVirtual.DelayedCall(1, () => { gameObject.SetActive(false); });
        }

        private void PlayWrongAnimation()
        {
            inputPanel.transform.DOShakePosition(0.5f, 0.1f);
            _image.DOColor(Color.red, 0.5f).OnComplete(() => _image.DOColor(_defaultColor, 0.5f));
        }

        private void PlayCorrectAnimation()
        {
            inputPanel.transform.DOShakePosition(0.5f, 0.1f);
            _image.DOColor(Color.green, 0.5f);
        }
    
        private void OnEnable()
        {
            SpellChecker.OnInputTextChanged += SetInputText;
            SpellChecker.OnWordEnded += OnWordEntered;
        }
        
        private void OnDisable()
        {
            SpellChecker.OnInputTextChanged -= SetInputText;
            SpellChecker.OnWordEnded -= OnWordEntered;
        }
       
    }
}
