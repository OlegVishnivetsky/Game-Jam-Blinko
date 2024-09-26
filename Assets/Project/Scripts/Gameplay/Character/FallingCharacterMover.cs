using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class FallingCharacterMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _stepTime;
        [SerializeField] private float _jumpForce;

        [SerializeField] private Rigidbody2D _bodyRigidbody;
        [SerializeField] private Rigidbody2D _leftLegRigidbody;
        [SerializeField] private Rigidbody2D _rightLegRigidbody;

        private Animator _animator;
        private bool _isMovingLeft;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Update()
        {
            if (_isMovingLeft)
            {
                StartCoroutine(MoveLeftRoutine());
            }
            else
            {
                StartCoroutine(MoveRightRoutine());
            }
        }

        public void Initialize(bool isMovingLeft)
        {
            _isMovingLeft = isMovingLeft;

            if (_isMovingLeft)
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
            _bodyRigidbody.AddForce(Vector2.up * _jumpForce, 
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