using System;
using System.Collections.Generic;
using System.IO;
using OLS_HyperCasual;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OLS_HyperCasual
{
    public class ResourcesController : BaseController
    {
        private Dictionary<string, Object> cachedPrefabs = new Dictionary<string, Object>();

        private static readonly string ResourceSuffix = "Resources/";
        
        public static GameSettingsSO GetSettings()
        {
            return BaseEntryPoint.Get<ResourcesController>().GetResource<GameSettingsSO>(ResourceConstants.GameSettings);
        }
        
        public List<T> GetAllResources<T>(string resourceFolder, bool isNeedCache = true) where T : Object
        {
            var resources = new List<T>();
            var files = Directory.GetFiles($"{Application.dataPath}/{resourceFolder}");
            foreach (var fileName in files)
            {
                if (fileName.EndsWith(".meta"))
                {
                    continue;
                }
                
                var lastResourceIndex = fileName.LastIndexOf(ResourceSuffix, StringComparison.Ordinal);
                var lastDotIndex = fileName.LastIndexOf(".", StringComparison.Ordinal);
                var startIndex = lastResourceIndex + ResourceSuffix.Length;
                var length = lastDotIndex - startIndex;
                var resourceName = fileName.Substring(startIndex, length);
                resources.Add(GetResource<T>(resourceName, isNeedCache));
            }

            return resources;
        }
        
        public T GetResource<T>(string resourceName, bool isNeedCache = true) where T : Object
        {
            if (cachedPrefabs.TryGetValue(resourceName, out var resource))
            {
                return resource as T;
            }

            resource = Resources.Load<T>(resourceName);
            if (isNeedCache)
            {
                cachedPrefabs.Add(resourceName, resource);
            }

            return (T) resource;
        }
        
        public void GetResourceAsync<T>(string resourceName, Action<T> callback, bool isNeedCache = true) where T : Object
        {
            if (cachedPrefabs.TryGetValue(resourceName, out var resource))
            {
                callback?.Invoke(resource as T);
                return;
            }

            var request = Resources.LoadAsync<T>(resourceName);
            request.completed += (operation) =>
            {
                if (operation.isDone == false)
                {
                    return;
                }

                resource = request.asset as T;
                if (resource == null)
                {
                    Debug.LogError("[ResourcesController.GetResourceAsync]: Asset not loaded: " + resourceName);
                    callback = null;
                    return;
                }

                if (isNeedCache && cachedPrefabs.ContainsKey(resourceName) == false)
                {
                    cachedPrefabs.Add(resourceName, resource);
                }

                callback?.Invoke(resource as T);
            };
        }
    }
}