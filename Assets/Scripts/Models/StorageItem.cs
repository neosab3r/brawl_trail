using UnityEngine;

namespace OLS_HyperCasual
{
    public abstract class StorageItem
    {
        public virtual Transform CachedTransform { get; }
        private StorageModel storageData;

        public void SetStorage(StorageModel newStorage)
        {
            storageData = newStorage;
        }

        public void UpdateStorage(StorageModel newStorage)
        {
            storageData?.RemoveFromStorage(this);
            storageData = newStorage;
            storageData?.AddToStorage(this);
        }
    }
}