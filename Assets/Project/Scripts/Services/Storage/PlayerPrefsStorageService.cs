using System;
using UnityEngine;
using Newtonsoft.Json;

namespace Services.Storage
{
    public class PlayerPrefsStorageService : IStorageService
    {
        public void Save(string key, object data, Action<bool> callback = null)
        {
            string file = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(key, file);
            callback?.Invoke(true);
        }

        public void Load<T>(string key, Action<T> callback)
        {
            if (PlayerPrefs.HasKey(key))
            {
                string file = PlayerPrefs.GetString(key);
                T data = JsonConvert.DeserializeObject<T>(file);
                callback?.Invoke(data);
            }
        }
    }
}
