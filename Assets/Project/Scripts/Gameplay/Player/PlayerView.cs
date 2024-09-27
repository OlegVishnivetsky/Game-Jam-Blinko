using Root;
using TMPro;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] Rope _rope;
        [SerializeField] Rigidbody2D _rb;
        [SerializeField] float _speed = 5;
        [SerializeField] private int _maxHealth;
        [SerializeField] private TextMeshProUGUI _healthText;

        [Inject]
        private GameStateHandler _gameStateHandler;

        [Inject]
        private SceneLoader _sceneLoader;

        private Animator _animator;
        private Vector3 _direction;
        private int _currentHealth;

        private bool _isMoving = false;

        private readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void Awake()
        {
            _currentHealth = _maxHealth;
            _healthText.text = $"{_currentHealth}/{_maxHealth}";

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

        public void TakeDamage()
        {
            if (_gameStateHandler.GetCurrentState() == GameState.Finish)
                return;

            if (_currentHealth - 1 <= 0)
            {
                _gameStateHandler.ChangeState(GameState.Finish);
                _sceneLoader.LoadSceneAsync("Gameplay");
            }

            _currentHealth--;
            _healthText.text = $"{_currentHealth}/{_maxHealth}";
        }

        public void SetMovingAnimation(bool isMoving)
        {
            _animator.SetBool(IsMoving, isMoving);
        }
    }
}
