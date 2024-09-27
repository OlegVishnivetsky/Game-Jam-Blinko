using R3;
using R3.Triggers;
using UnityEngine;

namespace Gameplay
{
    public class RopeElement : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        [field: SerializeField] public SpriteRenderer Renderer { get; private set; }
        [field: SerializeField] public CapsuleCollider2D Collider { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public HingeJoint2D Joint { get; private set; }
        [field: SerializeField] public Transform StartPoint { get; private set; }
        [field: SerializeField] public Transform EndPoint { get; private set; }

        private void Start()
        {
            this.OnCollisionEnter2DAsObservable()
                .Where(other => other.gameObject.GetComponent<BodyPart>())
                .Select(other => other.gameObject.GetComponent<BodyPart>())
                .Subscribe(part =>
                {
                    part.IsGrounded = true;
                    _particle.Play();
                    Destroy(part.transform.parent.gameObject);
                })
                .AddTo(this);
        }
    }
}
