using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Fields

        private const string IS_MOVING = "isMoving";
        private const float INPUT_THRESHOLD = 0.01f;
        private CharacterController _characterController;
        private Animator _animator;
        private Vector2 _movementInput;

        [SerializeField] private float moveSpeed = 5.0f;
        [SerializeField] private float rotationSpeed = 720f;
        
        private static readonly int IsMoving = Animator.StringToHash(IS_MOVING);

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            
            if (_characterController == null)
            {
                Debug.LogError("CharacterController component is missing on the player.");
            }
            
            if (_animator == null)
            {
                Debug.LogError("Animator component is missing on the player.");
            }
        }

        public void OnMove(InputValue value)
        {
            var input = value.Get<Vector2>();
    
            var swapped = new Vector2(input.y, -input.x);
            _movementInput = IsometricToCartesian(swapped);
        }
        private void Update()
        {
            MovePlayer();
            AnimatePlayer();
            RotatePlayer();
        }

        #endregion

        #region Private Methods

        private Vector2 IsometricToCartesian(Vector2 isoDirection)
        {
            Vector2 cartDirection = new Vector2(
                0.5f * (isoDirection.x - isoDirection.y),
                0.5f * (isoDirection.x + isoDirection.y));

            return cartDirection;
        }

        private void MovePlayer()
        {
            var move = new Vector3(_movementInput.x, 0, _movementInput.y) * moveSpeed;
            _characterController.Move(move * Time.deltaTime);
        }
        
        private void AnimatePlayer()
        {
            var isMoving = _movementInput.sqrMagnitude > INPUT_THRESHOLD;
            _animator.SetBool(IsMoving, isMoving);
        }

        private void RotatePlayer()
        {
            if (!(_movementInput.sqrMagnitude > INPUT_THRESHOLD)) return;
            
            var targetDirection = new Vector3(_movementInput.x, 0, _movementInput.y);
            var targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        #endregion
    }
}
