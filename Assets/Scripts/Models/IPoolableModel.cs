using UnityEngine;

namespace OLS_HyperCasual
{
    public interface IPoolableModel
    {
        public string PoolType { get; }
        public int PoolIndex { get; }
        public bool IsInPool { get; set; }


        T GetView<T>() where T : MonoBehaviour;
        void InitPoolModel(int poolIndex, string poolType);
    }
}