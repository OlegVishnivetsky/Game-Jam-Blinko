using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Root
{
    public class SceneLoader
    {
        public Subject<Unit> OnSceneLoadRequested = new();
        public Subject<Unit> OnSceneLoadRequestExecuted = new();

        private LoadingScreen _loadingScreen;

        public SceneLoader(LoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
        }

        public void LoadSceneAsync(string name)
        {
            Coroutines.StartRoutine(LoadScene(name));
        }

        private IEnumerator LoadScene(string sceneName)
        {
            _loadingScreen.Show();
            OnSceneLoadRequested.OnNext(new());

            if (Scenes.EMPTY != sceneName)
            {
                yield return SceneManager.LoadSceneAsync(Scenes.EMPTY);
            }
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return new WaitForSeconds(0.5f);

            _loadingScreen.Hide();
            OnSceneLoadRequestExecuted.OnNext(new());
        }
    }
}