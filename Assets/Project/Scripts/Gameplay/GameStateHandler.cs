using R3;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameStateHandler : IInitializable
    {
        private GameState _state = GameState.WaitBeforeStart;

        private Subject<GameState> _gameStateSubject = new Subject<GameState>();
        public Observable<GameState> GameStateObservable => _gameStateSubject;

        public void Initialize()
        {
            _gameStateSubject.OnNext(_state);
        }

        public GameState GetCurrentState()
        {
            return _state;
        }

        public void ChangeState(GameState state)
        {
            if (_state == state || _state == GameState.Finish) 
                return;

            Debug.Log(state);
            _state = state;
            _gameStateSubject.OnNext(state);
        }
    }

    public enum GameState
    {
        WaitBeforeStart,
        Start,
        Finish
    }
}