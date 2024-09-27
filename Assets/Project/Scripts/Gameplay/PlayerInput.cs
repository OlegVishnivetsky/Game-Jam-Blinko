using R3;
using System.Drawing;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] PlayerView _player;

        [Inject]
        private GameStateHandler _gameStateHandler;

        private bool _isActive;

        private void Start()
        {
            _gameStateHandler.GameStateObservable
                .Subscribe(state =>
                {
                    switch (state)
                    {
                        case GameState.WaitBeforeStart:
                        case GameState.Finish:
                            _isActive = false;
                            break;

                        case GameState.Start:
                            _isActive = true;
                            break;
                    }
                })
                .AddTo(this);
        }

        private void Update()
        {
            if (!_isActive)
                return;

            if(Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _player.SetMovingAnimation(true);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    _player.SetMovingAnimation(false);
                }

                if (touch.position.x < (Screen.width / 2))
                {
                    _player.MoveLeft();
                }
                else
                {
                    _player.MoveRight();
                }
            }
        }
    }
}