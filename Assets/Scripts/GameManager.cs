using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpellChecker spellChecker;
    private bool _isPaused = false;


    private void Update()
    {

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _isPaused = !_isPaused;
            spellChecker.SetEnableChecking(_isPaused);
            Debug.Log("IsPaused: " + _isPaused);
        }
    }


    private void OnInputEnded(bool isCorrect)
    {
        spellChecker.SetEnableChecking(false);
    }

    private void OnEnable()
    {
        SpellChecker.OnWordEnded += OnInputEnded;
    }

    private void OnDisable()
    {
        SpellChecker.OnWordEnded -= OnInputEnded;
    }
}