using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Fields

        private const string IS_MOVING = "isMoving";
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
            _movementInput = value.Get<Vector2>();
        }

        private void Update()
        {
            MovePlayer();
            AnimatePlayer();
            RotatePlayer();
        }

        #endregion

        #region Private Methods

        private void MovePlayer()
        {
            var move = new Vector3(-_movementInput.y, 0, _movementInput.x) * moveSpeed;
            _characterController.Move(move * Time.deltaTime);
        }
        
        private void AnimatePlayer()
        {
            var isMoving = _movementInput.sqrMagnitude > 0.05f;
            _animator.SetBool(IsMoving, isMoving);
        }

        private void RotatePlayer()
        {
            if (!(_movementInput.sqrMagnitude > 0.01f)) return;
            
            var targetDirection = new Vector3(-_movementInput.y, 0, _movementInput.x);
            var targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }


        #endregion
    }
}