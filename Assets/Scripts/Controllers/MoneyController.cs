using System;
using System.Collections.Generic;
using OLS_HyperCasual;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OLS_HyperCasual
{
    public class MoneyController : BaseController
    {
        public delegate void OnMoneyPickup(int moneyType, int moneyCount);
        
        public override bool HasFixedUpdate => true;

        private MoneyView moneyPrefab;
        private List<MoneyModel> moneyDataList = new List<MoneyModel>();
        private PlayerResourcesController playerResources;
        private OnMoneyPickup onMoneyPickupEvent;

        public MoneyController()
        {
            var resourcesController = BaseEntryPoint.Get<ResourcesController>();
            moneyPrefab = resourcesController.GetResource<MoneyView>(ResourceConstants.MONEY, false);

            playerResources = BaseEntryPoint.Get<PlayerResourcesController>();
        }

        public void CreateMoney(Vector3 position, int moneyGive)
        {
            var moneyView = Object.Instantiate(moneyPrefab);
            moneyView.transform.position = position + Vector3.up * 0.4f;
            moneyDataList.Add(new MoneyModel(moneyView, PResourceType.Soft, moneyGive));
        }

        public void SubscribePickup(OnMoneyPickup onMoneyPickup)
        {
            onMoneyPickupEvent += onMoneyPickup;
        }

        public void UnsubscribePickup(OnMoneyPickup onMoneyPickup)
        {
            onMoneyPickupEvent -= onMoneyPickup;
        }
        
        public void PickupMoney(MoneyModel data)
        {
            Object.Destroy(data.View.gameObject, 2f);
            data.ShowEffect();
            onMoneyPickupEvent?.Invoke((int)data.MoneyType, data.MoneyCount);
            
            moneyDataList.Remove(data);
        }

        private void RotateMoney(MoneyModel moneyData, float dt)
        {
            moneyData.CachedTransform.Rotate(Vector3.up * (moneyData.View.RotationSpeed * dt), Space.Self);
        }

        public int GetMoneyInRadius(Vector3 position, float radius, ref MoneyModel[] models)
        {
            int count = 0;
            for (int i = 0; i <= moneyDataList.Count; i++)
            {
                var moneyModel = moneyDataList[i];
                var moneyPosition = moneyModel.CachedTransform.position;
                moneyPosition.y = position.y;
                if (Vector3.Distance(moneyPosition, position) < radius)
                {
                    models[count] = moneyModel;
                    count++;
                }
            }

            return count;
        }

        public override void FixedUpdate(float dt)
        {
            for (int i = moneyDataList.Count - 1; i >= 0; i--)
            {
                var moneyData = moneyDataList[i];
                RotateMoney(moneyData, dt);
            }
        }
    }
}