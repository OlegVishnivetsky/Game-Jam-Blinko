using Lean.Pool;
using R3;
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
        [SerializeField] private float _delayBeforeStart;

        private LevelInfo _levelInfo;
        private GameStateHandler _gameStateHandler;

        private bool _isSpawnStarted;

        [Inject]
        public void Construct(LevelInfo levelInfo, GameStateHandler gameStateHandler)
        {
            _levelInfo = levelInfo;
            _gameStateHandler = gameStateHandler;
        }

        private void Start()
        {
            _gameStateHandler.GameStateObservable
                .Where(state => state == GameState.Start)
                .Subscribe(_ =>
                {
                    _isSpawnStarted = true;
                    StartCoroutine(StartSpawnRoutine());
                })
                .AddTo(this);

            _gameStateHandler.GameStateObservable
                .Where(state => state == GameState.Finish)
                .Subscribe(_ =>
                {
                    _isSpawnStarted = false;
                })
                .AddTo(this);
        }

        private IEnumerator StartSpawnRoutine()
        {
            yield return new WaitForSeconds(_delayBeforeStart);

            while (_isSpawnStarted)
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