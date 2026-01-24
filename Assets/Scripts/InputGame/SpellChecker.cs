using System;
using UnityEngine;

public class SpellChecker : MonoBehaviour
{
    
    [SerializeField] private string correctWord;
    
    public static event Action<string> OnInputTextChanged;
    public static event Action<bool> OnWordEnded;
    
    private string _currentWord;
    
    private void Awake()
    {
        _currentWord = "";
        UpdateUI();
    }
    
    public void SetEnableChecking(bool isEnabled)
    {
        if (isEnabled)
        {
            PlayerInputManager.OnKeyButtonTyped -= KeyButtonTyped;
            PlayerInputManager.OnKeyButtonTyped += KeyButtonTyped;
        }
        else
        {
            PlayerInputManager.OnKeyButtonTyped -= KeyButtonTyped;
        }
    }


    private void KeyButtonTyped(char letter)
    {
        _currentWord += letter;
        _currentWord = _currentWord.ToUpper();



        if (_currentWord[^1] != correctWord[_currentWord.Length - 1])
        {
            _currentWord = "";
            OnWordEnded?.Invoke(false);
        }

        if (_currentWord.Length >= correctWord.Length)
        {
            Debug.Log("Correct");
            OnWordEnded?.Invoke(true);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        string formatted = _currentWord;
        if (formatted.Length <= correctWord.Length)
        {
            // fill with '_' untill the length of the word is reached 
            int remainingLength = correctWord.Length - formatted.Length;
            for (int i = 0; i < remainingLength; i++)
            {
                formatted += '_';
            }
        }
        OnInputTextChanged?.Invoke(formatted);
    }
    
    private bool CheckWord()
    {
        for (int i = 0; i < correctWord.Length; i++)
        {
            if (_currentWord[i] != correctWord[i])
            {
                return false;
            }
        }

        return true;
    }
    
}

    

