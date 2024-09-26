using UnityEngine;

namespace Gameplay
{
    public class IgnoreBodyPartsCollision : MonoBehaviour
    {
        [SerializeField] private Collider2D[] _characterParts;

        private void Start()
        {
            for (int i = 0; i < _characterParts.Length; i++)
            {
                for (int j = i + 1; j < _characterParts.Length; j++)
                {
                    Physics2D.IgnoreCollision(_characterParts[i], _characterParts[j]);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<BodyPart>())
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), 
                    other.gameObject.GetComponent<Collider2D>());
            }
        }
    }
}