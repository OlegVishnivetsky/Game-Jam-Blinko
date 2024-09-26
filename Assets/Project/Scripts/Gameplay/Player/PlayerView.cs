using UnityEngine;

namespace Gameplay
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] Rope _rope;
        [SerializeField] Rigidbody2D _rb;
        [SerializeField] float _speed = 5;

        private Vector3 _direction;
        private bool _isMoving = false;

        private void Awake()
        {
            //_rope.Build();
        }

        private void FixedUpdate()
        {
            if (_isMoving)
            {
                transform.position += _direction * Time.deltaTime * _speed;
                //_rb.MovePosition(transform.position + _direction * Time.deltaTime * _speed);
            }
            _isMoving = false;
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


    }
}
