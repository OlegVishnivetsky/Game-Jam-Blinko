using OSSC;
using Services;
using Services.Currency;
using Services.Resource;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Zenject;

namespace Root
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] SoundController _soundController;
        [SerializeField] ResourcesConfig _resourcesConfig;

        private UIRootView _uiRoot;
        private SceneLoader _loader;

        public override void InstallBindings()
        {
            var prefabUIRoot = Resources.Load<UIRootView>("[INTERFACE]");
            _uiRoot = Instantiate(prefabUIRoot);
            DontDestroyOnLoad(_uiRoot.gameObject);

            _loader = new(_uiRoot.LoadingScreen);

            Container.Bind<SceneLoader>().FromInstance(_loader).AsSingle();
            Container.Bind<CurrencyService>().FromNew().AsSingle();
            //Container.Bind<ResourceService>().FromNew().AsSingle().WithArguments(_resourcesConfig);
            Container.Bind<AudioService>().FromNew().AsSingle().WithArguments(_soundController);
            Container.Bind<UIRootView>().FromInstance(_uiRoot).AsSingle();

            RunGame();
        }

        private void RunGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.GAMEPLAY)
            {
                _loader.LoadSceneAsync(Scenes.GAMEPLAY);
                return;
            }

            if (sceneName == Scenes.MAIN_MENU)
            {
                _loader.LoadSceneAsync(Scenes.MAIN_MENU);
            }

            if (sceneName != Scenes.BOOT)
            {
                return;
            }
#endif
            _loader.LoadSceneAsync(Scenes.MAIN_MENU);
        }
    }
}