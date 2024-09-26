using Root;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Testing
{
    [RequireComponent(typeof(Button))]
    public class SceneLoadingButton : MonoBehaviour
    {
        [SerializeField] string _sceneName;

        private SceneLoader _loader;

        [Inject]
        public void Construct(SceneLoader loader)
        {
            _loader = loader;
        }

        private void Awake()
        {
            var btn = GetComponent<Button>();
            btn.onClick.AddListener(Load);
        }

        private void Load()
        {
            _loader.LoadSceneAsync(_sceneName);
        }
    }
}