using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(BodyPart))]
    public class BodyPartBalancer : MonoBehaviour
    {
        [SerializeField] private float _targetRotation;
        [SerializeField] private float _restoreForce;

        private BodyPart _bodyPart;

        private void Awake()
        {
            _bodyPart = GetComponent<BodyPart>();
        }

        private void Update()
        {
            Balance();
        }

        public void DisableLimits()
        {
            _bodyPart.DisableLimits();
        }

        private void Balance()
        {
            _bodyPart.Rigidbody.MoveRotation(Mathf
                .LerpAngle(_bodyPart.Rigidbody.rotation, _targetRotation,
                _restoreForce * Time.deltaTime));
        }
    }
}