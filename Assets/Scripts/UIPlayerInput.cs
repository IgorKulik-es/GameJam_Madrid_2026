using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInput : MonoBehaviour
{
    
    [SerializeField] private TMP_Text inputText;
    [SerializeField] private GameObject inputPanel;
    
    private Color _defaultColor;
    private Image _image;

    private void Start()
    {
        inputText.text = "____";
        _defaultColor = inputPanel.GetComponent<Image>().color;
        _image = inputPanel.GetComponent<Image>();
    }


    private void OnEnable()
    {
        SpellChecker.OnInputTextChanged += SetInputText;
        SpellChecker.OnWordEnded += OnWordEntered;
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
        }
        else
        {
            PlayWrongAnimation();
        }
    }

    private void PlayWrongAnimation()
    {
        inputPanel.transform.DOShakePosition(0.5f, 0.1f);
        // red and back to default color
        _image.DOColor(Color.red, 0.5f).OnComplete(() => _image.DOColor(_defaultColor, 0.5f));
    }

    private void PlayCorrectAnimation()
    {
        inputPanel.transform.DOShakePosition(0.5f, 0.1f);
        // green and back to default color
        _image.DOColor(Color.green, 0.5f);
    }
    
    private void OnDisable()
    {
        SpellChecker.OnInputTextChanged -= SetInputText;
        SpellChecker.OnWordEnded -= OnWordEntered;
    }

}
