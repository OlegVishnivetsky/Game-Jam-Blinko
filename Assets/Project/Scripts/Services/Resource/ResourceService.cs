using R3;
using Services.Storage;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Resource
{
    public class ResourceService
    {
        private const string SAVE_KAY_BASE = "Res";

        private Dictionary<string, ResourceInfo> _resourceInfoMap = new();
        private Dictionary<string, float> _resourceValueMap = new();
        private IStorageService _storage = new PlayerPrefsStorageService();

        public readonly Subject<ResourceValueChangedArgs> OnResourceValueChanged = new();

        public ResourceService(ResourcesConfig config)
        {
            foreach(var info in config.Resources)
            {
                _resourceInfoMap.Add(info.ID, info);
                _resourceValueMap.Add(info.ID, info.DefaultValue);
                _storage.Load<float>(GetSaveKey(info.ID), val => _resourceValueMap[info.ID] = val);
            }
        }

        public float GetValue(string id)
        {
            if (IsIDValid(id) == false) return default;
            return _resourceValueMap[id];
        }


        public bool TrySetValue(string id, float amount)
        {
            if (IsIDValid(id) == false || IsEnoughResource(id, amount) == false) return false;
            if (amount < _resourceInfoMap[id].MinValue || amount > _resourceInfoMap[id].MaxValue) return false;

            _resourceValueMap[id] = amount;
            _storage.Save(GetSaveKey(id), _resourceValueMap[id]);

            OnResourceValueChanged?.OnNext(new(id, _resourceValueMap[id]));
            return true;
        }

        public bool TryChangeValue(string id, float amount)
        {
            if(IsIDValid(id) == false) return false;
            if (amount < _resourceInfoMap[id].MinValue || IsEnoughResource(id, amount) == false ) return false;

            _resourceValueMap[id] += amount;
            _storage.Save(GetSaveKey(id), _resourceValueMap[id]);

            OnResourceValueChanged?.OnNext(new(id, _resourceValueMap[id]));
            return true;
        }

        public bool IsEnoughResource(string id, float value)
        {
            if (IsIDValid(id) == false) return false;
            return Mathf.Abs(_resourceValueMap[id] - _resourceInfoMap[id].MinValue) >= Mathf.Abs(value);
        }

        public void UpdateAmount()
        {
            foreach (var info in _resourceInfoMap.Values)
            {
                _storage.Load<float>(GetSaveKey(info.ID), val =>
                {
                    _resourceValueMap[info.ID] = val;
                    OnResourceValueChanged?.OnNext(new(info.ID, _resourceValueMap[info.ID]));
                });
            }
        }

        private bool IsIDValid(string id)
        {
            if (_resourceInfoMap.ContainsKey(id) == false)
            {
                Debug.LogError($"Argument exception. ResourceService has not such ID {id}.");
                return false;
            }
            return true;
        }

        private string GetSaveKey(string resID)
        {
            return SAVE_KAY_BASE + resID;
        }
    }
}
