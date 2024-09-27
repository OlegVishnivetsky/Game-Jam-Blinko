using Root;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] GameObject _sceneUIPrefab;
        [SerializeField] private LevelInfo _levelInfo;
        [SerializeField] private PlayerView _playerView;

        private UIRootView _uiRoot;

        [Inject]
        public void Construct(UIRootView uiRoot)
        {
            _uiRoot = uiRoot;
        }

        public override void InstallBindings()
        {
            Container
                .BindInstance(_levelInfo)
                .AsSingle()
                .NonLazy();

            Container.
                BindInstance(_playerView)
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<GameStateHandler>()
                .AsSingle()
                .NonLazy();

            _uiRoot.AttachSceneUI(_sceneUIPrefab, Container);
        }
    }
}