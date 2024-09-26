using System;
using UnityEngine;

namespace Services.Resource
{
    [Serializable]
    public class ResourceInfo
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public float MinValue { get; private set; }
        [field: SerializeField] public float MaxValue { get; private set; }
        [field: SerializeField] public float DefaultValue { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }

        public ResourceInfo() 
        {
            ID = "";
            MinValue = float.MinValue;
            MaxValue = float.MaxValue;
            DefaultValue = 0f;
            Icon = null;
        }
    }
}
