using UnityEngine;

namespace Gameplay
{
    public class RopeElement : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer Renderer { get; private set; }
        [field: SerializeField] public CapsuleCollider2D Collider { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public HingeJoint2D Joint { get; private set; }
        [field: SerializeField] public Transform StartPoint { get; private set; }
        [field: SerializeField] public Transform EndPoint { get; private set; }
    }

    public class Rope : MonoBehaviour
    {
        [SerializeField] RopeElement _ropePrefab;

        private void Awake()
        {
            
        }
    }
}
