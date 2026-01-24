using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class BusController : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;

    [SerializeField] private float accelerationSpeed = 0.05f;
    [SerializeField] private float decelerationSpeed = 0.05f;
    [SerializeField] private float maxAcceleration = 1000f;
    [SerializeField] private float minDeceleration = 0f;
    
    [SerializeField] private BackgroundMover backgroundMover;
    [SerializeField] private StoppingZone stoppingZone;
    
    private float _multiplier;

    public enum BackgroundState
    {
        Moving,
        Accelerating,
        Decelerating,
        Stopped
    }
    
    private BackgroundState currentState = BackgroundState.Stopped;

    private void Update()
    {

        // Replaced Legacy Input with New Input System
        if (Keyboard.current != null)
        {
            if (Keyboard.current.bKey.isPressed)
            {
                UpdateStatement(BackgroundState.Accelerating);
            }

            if (Keyboard.current.vKey.isPressed)
            {
                UpdateStatement(BackgroundState.Decelerating);
            }
        }
        
        var delta = speed * _multiplier * Time.deltaTime;
        
        backgroundMover.ExternalUpdate(delta);
        stoppingZone.ExternalUpdate(delta);

        if (delta == 0 && currentState != BackgroundState.Stopped)
        {
            if (stoppingZone.CheckIfPlayerIsInStoppingZone(transform.position.x))
            {
                Debug.Log("You are in the stopping zone");
            }
            else
            {
                Debug.Log("!!!You are not in the stopping zone!!!");
            }
        }
    }

    private void UpdateStatement(BackgroundState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case BackgroundState.Accelerating:
                _multiplier += accelerationSpeed;
                _multiplier = Mathf.Min(maxAcceleration, _multiplier);
                break;
            case BackgroundState.Decelerating:
                _multiplier -= decelerationSpeed;
                _multiplier = Mathf.Max(minDeceleration, _multiplier);
                break;
        }
    }
}
