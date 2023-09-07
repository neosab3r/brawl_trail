using UnityEngine;

namespace OLS_HyperCasual
{
    public struct PooledObject
    {
        public int PooledIndex { get; }
        public MonoBehaviour Obj { get; }
        
        public PooledObject(int pooledIndex, MonoBehaviour obj)
        {
            PooledIndex = pooledIndex;
            Obj = obj;
        }
    }
}