using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] Rope _rope;
        [SerializeField] Rigidbody2D _rb;
        [SerializeField] float _speed = 5;

        private Animator _animator;
        private Vector3 _direction;
        private bool _isMoving = false;

        private readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void Awake()
        {
            //_rope.Build();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (_isMoving)
            {
                transform.position += _direction * Time.deltaTime * _speed;

            }
            _isMoving = false;

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 
                -6f, 6f), transform.position.y, transform.position.z);
        }

        public void MoveLeft()
        {
            _isMoving = true;
            _direction = Vector3.left;
        }

        public void MoveRight()
        {
            _isMoving = true;
            _direction = Vector3.right;
        }

        public void SetMovingAnimation(bool isMoving)
        {
            _animator.SetBool(IsMoving, isMoving);
        }
    }
}
