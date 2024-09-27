using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BodyPart : MonoBehaviour
    {
        [Title("Disabled Limits")]
        [SerializeField] private float _lowerAngle;
        [SerializeField] private float _upperAngle; 

        private HingeJoint2D _hingeJoint2D;
        private Rigidbody2D _rigidbody2D;

        public bool IsGrounded;

        public Rigidbody2D Rigidbody => _rigidbody2D;

        private void Awake()
        {
            _hingeJoint2D = GetComponent<HingeJoint2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void DisableLimits()
        {
            if ( _hingeJoint2D != null)
            {
                JointAngleLimits2D disabledLimints = _hingeJoint2D.limits;
                disabledLimints.min = _lowerAngle;
                disabledLimints.max = _upperAngle;
                _hingeJoint2D.limits = disabledLimints;
            }
        }
    }
}