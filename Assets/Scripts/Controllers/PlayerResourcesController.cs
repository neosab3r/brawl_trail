using System.Collections.Generic;

namespace OLS_HyperCasual
{
    public sealed class PlayerResourcesController : BaseController
    {
        private List<PlayerResourceModel> resources = new List<PlayerResourceModel>();

        public static readonly string SoftInitialKey = "SoftInitial";
        
        public PlayerResourcesController(bool isInitGameResources)
        {
            InitResources(isInitGameResources);
        }

		public void InitResourceData(int resourceType, int initialCount, int maxCount = -1)
        {
            if (HasResource(resourceType))
            {
                return;
            }
            
            resources.Add(new PlayerResourceModel(resourceType, initialCount, maxCount));
        }

        public void SetResourceView(int resourceType, UIResourceView view)
        {
            var data = GetResourceModel(resourceType);
            data.SetView(view);
        }

        public void AddResourceValue(int resourceType, int addCount)
        {
            var resourceData = GetResourceModel(resourceType);
            resourceData.AddResource(addCount);
        }

        public void SpendResourceValue(int resourceType, int spendCount)
        {
            var resourceData = GetResourceModel(resourceType);
            resourceData.SpendResource(spendCount);
        }

        public int GetResourceCount(int resourceType)
        {
            return GetResourceModel(resourceType).CurrentAmount;
        }

        public PlayerResourceModel GetResourceModel(int resourceType)
        {
            var typeIndex = resourceType;
            foreach (var resourceData in resources)
            {
                if (resourceData.ResourceType == typeIndex)
                {
                    return resourceData;
                }
            }

            return null;
        }
        
        private void InitResources(bool isInitGameResources)
        {
            if (isInitGameResources)
            {
                var settings = BaseEntryPoint.Get<ResourcesController>().GetResource<GameSettingsSO>(ResourceConstants.GameSettings);
                var initialSoftCount = settings.GetIntValue(SoftInitialKey, false);
                
                InitResourceData(PResourceType.Soft, initialSoftCount);
            }
            
            InitResourceData(PResourceType.GameLevel, 0);
        }

        private bool HasResource(int resourceType)
        {
            foreach (var resource in resources)
            {
                if (resource.ResourceType == resourceType)
                {
                    return true;
                }
            }

            return false;
        }
    }
}