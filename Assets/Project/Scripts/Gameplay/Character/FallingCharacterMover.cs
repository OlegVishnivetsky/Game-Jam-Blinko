using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class FallingCharacterMover : MonoBehaviour
    {
        [Title("Movement")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _stepTime;

        [Title("Jumping")]
        [MinMaxSlider(0f, 100f)]
        [SerializeField] private Vector2 _jumpUpForce;
        [MinMaxSlider(0f, 100f)]
        [SerializeField] private Vector2 _jumpSideForce;

        [SerializeField] private Rigidbody2D _bodyRigidbody;
        [SerializeField] private Rigidbody2D _leftLegRigidbody;
        [SerializeField] private Rigidbody2D _rightLegRigidbody;

        private Animator _animator;
        private MoveDirection _moveDirection = MoveDirection.Right;
        private bool _isJumped;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Update()
        {
            if (_moveDirection == MoveDirection.Left)
            {
                StartCoroutine(MoveLeftRoutine());
            }
            else
            {
                StartCoroutine(MoveRightRoutine());
            }
        }

        public void Initialize(MoveDirection moveDirection)
        {
            _moveDirection = moveDirection;

            if (_moveDirection == MoveDirection.Left)
            {
                _animator.Play("FallingCharacter_WalkLeft");
            }
            else
            {
                _animator.Play("FallingCharacter_WalkRight");
            }
        }

        public void Jump()
        {
            if (_isJumped)
                return;

            _isJumped = true;

            float randomUpForce = Random.Range(_jumpUpForce.x, 
                _jumpUpForce.y);
            float randomSideForce = Random.Range(_jumpSideForce.x,
                _jumpSideForce.y);

            Vector2 sideForceDirection = _moveDirection == MoveDirection.Left
                    ? Vector2.left : Vector2.right;

            _bodyRigidbody.AddForce(Vector2.up * randomUpForce, 
                ForceMode2D.Impulse);           
            _bodyRigidbody.AddForce(sideForceDirection * randomSideForce,
                ForceMode2D.Impulse);
        }

        private IEnumerator MoveLeftRoutine()
        {
            Debug.Log("Moving left");
            MoveRigidbody(_leftLegRigidbody, Vector2.left);
            yield return new WaitForSeconds(_stepTime);
            MoveRigidbody(_rightLegRigidbody, Vector2.left);
        }

        private IEnumerator MoveRightRoutine()
        {
            Debug.Log("Moving right");
            MoveRigidbody(_leftLegRigidbody, Vector2.right);
            yield return new WaitForSeconds(_stepTime);
            MoveRigidbody(_rightLegRigidbody, Vector2.right);
        }

        private void MoveRigidbody(Rigidbody2D rigidbody2D, Vector2 direction)
        {
            _leftLegRigidbody.AddForce(direction * _moveSpeed * Time.deltaTime);
        }
    }
}