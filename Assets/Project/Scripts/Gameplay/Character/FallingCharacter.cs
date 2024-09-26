using System.Collections;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(FallingCharacterMover))]
    [RequireComponent(typeof(FallingCharacterCollisionHandler))]
    public class FallingCharacter : MonoBehaviour
    {
        private FallingCharacterMover characterMover;
        private FallingCharacterCollisionHandler collisionHandler;

        private void Awake()
        {
            characterMover = GetComponent<FallingCharacterMover>(); 
            collisionHandler = GetComponent<FallingCharacterCollisionHandler>();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2);

            characterMover.Jump();
            collisionHandler.Toggle(false);
        }
    }
}