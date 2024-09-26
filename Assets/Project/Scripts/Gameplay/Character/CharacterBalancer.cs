using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterBalancer : MonoBehaviour
    {
        [SerializeField] private float _targetRotation;
        [SerializeField] private float _restoreForce;

        private new Rigidbody2D rigidbody2D;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Balance();
        }

        private void Balance()
        {
            rigidbody2D.MoveRotation(Mathf
                .LerpAngle(rigidbody2D.rotation, _targetRotation,
                _restoreForce * Time.deltaTime));
        }
    }
}