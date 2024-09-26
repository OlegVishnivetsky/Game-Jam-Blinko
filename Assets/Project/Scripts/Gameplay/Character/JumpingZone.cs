using UnityEngine;

namespace Gameplay
{
    public class JumpingZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        { 
            if (other.gameObject.TryGetComponent(out BodyPart bodyPart))
            {
                bodyPart.GetComponentInParent<FallingCharacterMover>().Jump();
            }
        }
    }
}