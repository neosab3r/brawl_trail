using System;
using System.Collections.Generic;
using UnityEngine;

namespace OLS_HyperCasual
{
    public class StorageModel
    {
        public readonly List<StorageItem> storageItems = new List<StorageItem>(40);
        public string StorageTypeName { get; }
        public int StorageCount => storageItems.Count;
        public int MaxStorageCount { get; }
        public Action<StorageItem> OnAdded, OnRemoved;

        public StorageModel(string storageTypeName, int maxStorageCount)
        {
            this.StorageTypeName = storageTypeName;
            MaxStorageCount = maxStorageCount;
        }

        public bool HasStorageSpace()
        {
            return StorageCount + 1 <= MaxStorageCount;
        }

        public void AddToStorage(StorageItem item)
        {
            item.SetStorage(this);
            storageItems.Add(item);
            OnAdded?.Invoke(item);
        }

        public void RemoveFromStorage(StorageItem item)
        {
            item.SetStorage(null);
            storageItems.Remove(item);
            OnRemoved?.Invoke(item);
        }
    }
}