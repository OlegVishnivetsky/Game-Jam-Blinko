using System.Collections.Generic;
using UnityEngine;

namespace Services.Resource
{
    [CreateAssetMenu(fileName = "ResourcesConfig", menuName = "Congigs/ResourcesConfig")]
    public class ResourcesConfig : ScriptableObject
    {
        [field:SerializeField] public List<ResourceInfo> Resources { get; private set; }

        private void OnValidate()
        {
            HashSet<string> ids = new();
            for(int i = 0; i < Resources.Count; i++)
            {
                var r = Resources[i];
                if (ids.Contains(r.ID))
                {
                    throw new System.ArgumentException($"List already contains key \"{r.ID}\"");
                }

                if(r.MinValue >= r.MaxValue)
                {
                    throw new System.ArgumentException($"MinValue is bigger than MaxValue in item with key \"{r.ID}\"");
                }

                /*                if (r.MinValue > r.MaxValue)
                                {
                                    throw new System.ArgumentException($"MinValue is bigger than MaxValue in item with key \"{r.ID}\"");
                                }*/
                ids.Add(r.ID);
            }
        }
    }
}
