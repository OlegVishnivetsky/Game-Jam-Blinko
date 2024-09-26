using System.Collections;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(FallingCharacterMover))]
    [RequireComponent(typeof(FallingCharacterCollisionHandler))]
    public class FallingCharacter : MonoBehaviour
    {
        private FallingCharacterMover _characterMover;
        private FallingCharacterCollisionHandler _collisionHandler;

        public FallingCharacterMover CharacterMover => _characterMover;

        private void Awake()
        {
            _characterMover = GetComponent<FallingCharacterMover>(); 
            _collisionHandler = GetComponent<FallingCharacterCollisionHandler>();
        }
    }
}