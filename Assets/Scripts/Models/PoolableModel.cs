using UnityEngine;

namespace OLS_HyperCasual
{
    public class PoolableModel<T> : BaseModel<T>, IPoolableModel where T : MonoBehaviour
    {
        public string PoolType { get; private set; }
        public int PoolIndex { get; private set; }
        public bool IsInPool { get; set; }

        public void InitPoolModel(int poolIndex, string poolType)
        {
            PoolIndex = poolIndex;
            PoolType = poolType;
        }
        
        public K GetView<K>() where K : MonoBehaviour
        {
            return View as K;
        }
    }
}