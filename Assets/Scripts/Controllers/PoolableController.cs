using System;
using System.Collections.Generic;
using UnityEngine;

namespace OLS_HyperCasual
{
    public class PoolableController <T, KT> : BaseController where T : IPoolableModel where KT: MonoBehaviour
    {
        private PoolController poolController;
        protected Dictionary<string, Dictionary<int, T>> pooledModelsDict = new();
        
        private int indexCounter;

        public PoolableController()
        {
            poolController = BaseEntryPoint.Get<PoolController>();
        }
        
        public void PreInitPool(string poolType, KT prefab)
        {
            var pool = new Dictionary<int, T>();
            pooledModelsDict.Add(poolType, pool);
            poolController.InitPool(poolType, prefab, OnPoolObjectCreated, GetPoolObjectIndex);

            foreach (var kv in pool)
            {
                kv.Value.IsInPool = true;
            }
        }

        public T GetFromPool(string key)
        {
            var pooledObject = poolController.GetFromPool(key);
            if (pooledObject.Obj == null)
            {
                return default;
            }

            var typeDict = pooledModelsDict[key];
            var model = typeDict[pooledObject.PooledIndex];
            model.IsInPool = false;
            
            return model;
        }

        public void ReturnToPool(T poolableModel)
        {
            var view = poolableModel.GetView<KT>();
            poolableModel.IsInPool = true;
            poolController.ReturnToPool(poolableModel.PoolType, poolableModel.PoolIndex, view);
        }
        
        private int GetPoolObjectIndex()
        {
            return indexCounter++;
        }
        
        protected virtual void OnPoolObjectCreated(string poolType, int objIndex, MonoBehaviour view)
        {
            var targetView = view as KT;
            
            var model = (T)Activator.CreateInstance(typeof(T), new object[] { targetView });
            model.InitPoolModel(objIndex, poolType);

            var typeDict = pooledModelsDict[poolType];
            typeDict.Add(objIndex, model);
        }
    }
}