using Root;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] GameObject _sceneUIPrefab;

        private UIRootView _uiRoot;

        [Inject]
        public void Construct(UIRootView uiRoot)
        {
            _uiRoot = uiRoot;
        }

        public override void InstallBindings()
        {


            _uiRoot.AttachSceneUI(_sceneUIPrefab, Container);
        }
    }
}