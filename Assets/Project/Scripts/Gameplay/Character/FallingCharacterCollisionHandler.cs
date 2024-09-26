using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class FallingCharacterCollisionHandler : MonoBehaviour
    {
        [SerializeField] private List<Collider2D> _mainColliders;
        [SerializeField] private List<BodyPartBalancer> bodyBalancers;
        [SerializeField] private CircleCollider2D _circleCollider;

        public void Toggle(bool isMainEnabled)
        {
            SetCollidersStatus(isMainEnabled, 
                !isMainEnabled);
        }

        private void SetCollidersStatus(bool mainColliderEnabled, 
            bool circleColliderEnabled)
        {
            //foreach (Collider2D collider in _mainColliders)
            //{
            //    collider.enabled = mainColliderEnabled;
            //}

            foreach (BodyPartBalancer bodyPartBalancer in bodyBalancers)
            {
                bodyPartBalancer.enabled = mainColliderEnabled;
                bodyPartBalancer.DisableLimits();
            }

            //_circleCollider.enabled = circleColliderEnabled;
        }
    }
}