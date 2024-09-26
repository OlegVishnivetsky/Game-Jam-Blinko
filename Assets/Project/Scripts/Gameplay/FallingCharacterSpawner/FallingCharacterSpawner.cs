using Lean.Pool;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class FallingCharacterSpawner : MonoBehaviour
    {
        [SerializeField] private FallingCharacter _fallingCharacter;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private MoveDirection _moveDirection;

        private LevelInfo _levelInfo;

        [Inject]
        public void Construct(LevelInfo levelInfo)
        {
            _levelInfo = levelInfo;
        }

        private IEnumerator Start()
        {
            while (true)
            {
                float delayBeforeSpawn = Random.Range(_levelInfo.MinSpawnTime,
                    _levelInfo.MaxSpawnTime);

                yield return new WaitForSeconds(delayBeforeSpawn);

                Spawn();
            }
        }

        public void Spawn()
        {
            FallingCharacter character = LeanPool.Spawn(_fallingCharacter);
            character.transform.position = _spawnPoint.position;
            character.CharacterMover.Initialize(_moveDirection);
        }
    }
}