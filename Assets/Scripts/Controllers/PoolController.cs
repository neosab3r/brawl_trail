using System;
using System.Collections.Generic;
using UnityEngine;

namespace OLS_HyperCasual
{
    public class PoolController : BaseController
    {
        private Dictionary<string, PoolModel> poolsDict = new();

        public void InitPool(string key, MonoBehaviour prefab, Action<string, int, MonoBehaviour> onPoolObjectCreated, Func<int> getPoolObjectIndex)
        {
            var model = new PoolModel(key, prefab, onPoolObjectCreated, getPoolObjectIndex);
            poolsDict.Add(key, model);
        }

        public PooledObject GetFromPool(string key)
        {
            if (poolsDict.TryGetValue(key, out var pool))
            {
                return pool.GetFromPool();
            }

            Debug.LogError($"[{nameof(PoolController)}.{nameof(GetFromPool)}]: Cannot find pool {key}");
            return default;
        }

        public void ReturnToPool<T>(string key, int objIndex, T obj) where T : MonoBehaviour
        {
            if (poolsDict.TryGetValue(key, out var pool))
            {
                pool.ReturnToPool(obj, objIndex);
                return;
            }
        
            Debug.LogError($"[{nameof(PoolController)}.{nameof(ReturnToPool)}]: Cannot find pool {key}");
        }
    }
}