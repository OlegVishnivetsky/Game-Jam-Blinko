using UnityEngine;
using Zenject;

namespace Root
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private Transform _uiSceneContainer;

        public LoadingScreen LoadingScreen => _loadingScreen;

        public void AttachSceneUI(GameObject sceneUIPrefab, DiContainer container)
        {
            ClearSceneUI();
            var sceneUI = container.InstantiatePrefab(sceneUIPrefab);
            sceneUI.transform.SetParent(_uiSceneContainer, false);
        }

        private void ClearSceneUI()
        {
            var childCount = _uiSceneContainer.childCount;
            for (var i = 0; i < childCount; i++)
            {
                Destroy(_uiSceneContainer.GetChild(i).gameObject);
            }
        }
    }
}