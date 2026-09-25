using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectD.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActions;
        [SerializeField] private float _jumpVelocity = 5;
        [SerializeField] private float _moveVelocity = 5;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundCheckDistance = 0.6f;

        private InputAction _moveAction;
        private InputAction _jumpAction;

        private Rigidbody2D _rigidbody;
        private Vector2 _moveValue;
        private bool _isJump;
        private Transform _transform;

        private void OnEnable()
        {
            _inputActions.FindActionMap("Player").Enable();
        }

        private void OnDisable()
        {
            _inputActions.FindActionMap("Player").Disable();
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
            _moveAction = _inputActions.FindAction("Move");
            _jumpAction = _inputActions.FindAction("Jump");
        }

        private void Update()
        {
            _moveValue = _moveAction.ReadValue<Vector2>(); 
            _isJump = _jumpAction.WasPressedThisFrame();

            if (_isJump)
            {
                Jump();
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            Vector3 position = _transform.position;

            if (_moveValue.x < 0f)
            {
                position.x += _moveValue.x * _moveVelocity * Time.deltaTime;
                // TO DO: flip sprite
            }
            else if (_moveValue.x > 0f)
            {
                position.x += _moveValue.x * _moveVelocity * Time.deltaTime;
                // TO DO: normal sprite
            }

            _transform.position = position;
        }

        public void Jump()
        {
            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * _jumpVelocity, ForceMode2D.Impulse);
            }
        }

        bool IsGrounded()
        {
            return Physics2D.Raycast(
                _transform.position,
                Vector2.down,
                _groundCheckDistance,
                _groundLayer
            );
        }
    }
}
