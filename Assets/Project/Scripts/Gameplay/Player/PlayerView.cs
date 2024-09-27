using UnityEngine;
using R3;
using R3.Triggers;
using Lean.Pool;

namespace Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] Rope _rope;
        [SerializeField] Collider2D _trigger;
        [SerializeField] float _speed = 5;
        [SerializeField] ParticleSystem _hit1PRefab;
        [SerializeField] ParticleSystem _hit2PRefab;

        private Animator _animator;
        private Vector3 _direction;
        private bool _isMoving = false;

        private readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void Awake()
        {
            //_rope.Build();
            _animator = GetComponent<Animator>();
            _trigger.OnTriggerEnter2DAsObservable().Subscribe(OnCatchObject).AddTo(this);
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

        private void OnCatchObject(Collider2D obj)
        {
            var fx = LeanPool.Spawn(_hit1PRefab);
            fx.transform.position = obj.transform.position;
            fx.Play();
            LeanPool.Despawn(obj);
        }
    }
}
