using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    // Singleton instance for easy access
    public static PlayerInputManager Instance { get; private set; }

    // Event that other scripts can subscribe to
    public static event Action<char> OnKeyButtonTyped;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // Subscribe to the global text input event
        if (Keyboard.current != null)
        {
            Keyboard.current.onTextInput += HandleTextInput;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        if (Keyboard.current != null)
        {
            Keyboard.current.onTextInput -= HandleTextInput;
        }
    }

    private void HandleTextInput(char character)
    {
        OnKeyButtonTyped?.Invoke(character);
    }
}
