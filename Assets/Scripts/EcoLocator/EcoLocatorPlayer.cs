using UnityEngine;
using UnityEngine.InputSystem;

namespace EcoLocator
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EcoLocatorPlayer : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        
        private Rigidbody2D _rb;
        private InputAction _moveAction;
        private Vector2 _moveInput;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.gravityScale = 0;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _rb.linearDamping = 0; 

            // Setup WASD Input
            _moveAction = new InputAction("Move", binding: "<Gamepad>/leftStick");
            _moveAction.AddCompositeBinding("Dpad")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
            
            
        }

        private void OnEnable()
        {
            _moveAction.Enable();
        }

        private void OnDisable()
        {
            _moveAction.Disable();
        }

        private void Update()
        {
            _moveInput = _moveAction.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            // Move using velocity for physics collisions
            _rb.linearVelocity = _moveInput * speed;
        }
    }
}
