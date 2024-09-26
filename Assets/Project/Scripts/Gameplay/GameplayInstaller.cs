using Root;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] GameObject _sceneUIPrefab;
        [SerializeField] private LevelInfo _levelInfo;

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

            _uiRoot.AttachSceneUI(_sceneUIPrefab, Container);
        }
    }
}