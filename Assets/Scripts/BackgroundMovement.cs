using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private GameObject[] backgrounds;
    [SerializeField] private Vector2 boundaries;
    [SerializeField] private Vector2 resetPosition;
    
    [SerializeField] private float accelerationTime = 2;
    [SerializeField] private float decelerationSpeed = 2;

    private float _multiplier;

    public enum BackgroundState
    {
        Moving,
        Accelerating,
        Decelerating
    }
    
    private BackgroundState currentState = BackgroundState.Moving;

    private void Update()
    {
        foreach (var bg in backgrounds)
        {
            var pos = bg.transform.position;
            pos.x -= speed * Time.deltaTime * _multiplier;
            if (pos.x < boundaries.x)
            {
                pos.x = resetPosition.x;
                pos.y = resetPosition.y;
            }
            
            bg.transform.position = pos;
        }

        // Replaced Legacy Input with New Input System
        if (Keyboard.current != null)
        {
            if (Keyboard.current.bKey.wasPressedThisFrame)
            {
                
                SetStatement(BackgroundState.Accelerating);
            }

            if (Keyboard.current.vKey.wasReleasedThisFrame)
            {
                SetStatement(BackgroundState.Decelerating);
            }
        }
    }

    public void SetStatement(BackgroundState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case BackgroundState.Moving:
                _multiplier = 1;
                break;
            case BackgroundState.Accelerating:
                DOVirtual.Float(0f,1f,accelerationTime, value => _multiplier = value);
                break;
            case BackgroundState.Decelerating:
                DOVirtual.Float(1f,0f,decelerationSpeed, value => _multiplier = value);
                break;
        }
    }
}
