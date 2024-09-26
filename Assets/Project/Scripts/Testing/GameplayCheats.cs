using Services.Resource;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Testing
{
    public class GameplayCheats : MonoBehaviour
    {
        [SerializeField] string resourceID;
        [SerializeField] float resourceSettingValue;
        [SerializeField] float resourceAddingValue;

        //[Inject] ResourceService _resourceService;


/*        [Button]
        private void AddResource()
        {
            _resourceService.TryChangeValue(resourceID, resourceAddingValue);
        }

        [Button]
        private void SetResource()
        {
            _resourceService.TryChangeValue(resourceID, resourceSettingValue);
        }*/

        [Button]
        private void TST()
        {
        }
    }
}