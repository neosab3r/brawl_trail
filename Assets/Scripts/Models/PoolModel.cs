using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OLS_HyperCasual
{
    public class PoolModel
    {
        private Queue<PooledObject> poolQueue = new();
        private string key;
        private MonoBehaviour prefab;
        private Func<int> getPoolObjectIndex;
        private Action<string, int, MonoBehaviour> onPoolObjectCreated;
        private int indexer;
        private int addPerLimit;

        public PoolModel(string key, MonoBehaviour prefab, Action<string, int, MonoBehaviour> onPoolObjectCreated, Func<int> getPoolObjectIndex, int addPerLimit = 1, int initialAmount = 1)
        {
            this.key = key;
            this.prefab = prefab;
            this.addPerLimit = addPerLimit;
            this.getPoolObjectIndex = getPoolObjectIndex;
            this.onPoolObjectCreated = onPoolObjectCreated;
            AddPoolObjects(initialAmount);
        }

        public PooledObject GetFromPool()
        {
            if (poolQueue.Count == 0)
            {
                AddPoolObjects(addPerLimit);
            }

            return poolQueue.Dequeue();
        }

        public void ReturnToPool(MonoBehaviour obj, int objIndex)
        {
            poolQueue.Enqueue(new PooledObject(objIndex, obj));
        }
    
        private void AddPoolObjects(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var obj = Object.Instantiate(prefab);
                obj.gameObject.SetActive(false);
                var index = getPoolObjectIndex();
                
                poolQueue.Enqueue(new PooledObject(index, obj));
                onPoolObjectCreated?.Invoke(key, index, obj);
            }
        }
    }
}