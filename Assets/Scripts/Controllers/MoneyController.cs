using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using OLS_HyperCasual;
using Photon.Pun;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OLS_HyperCasual
{
    public class MoneyController : BaseController
    {
        public delegate void OnMoneyPickup(int moneyType, int moneyCount);

        private Hashtable pickUpCoinHash = new Hashtable();

        public override bool HasFixedUpdate => true;

        private MoneyView moneyPrefab;
        private List<MoneyModel> moneyDataList = new List<MoneyModel>();
        private OnMoneyPickup onMoneyPickupEvent;

        private PlayerController playerController;

        public MoneyController()
        {
            var resourcesController = BaseEntryPoint.Get<ResourcesController>();
            moneyPrefab = resourcesController.GetResource<MoneyView>(ResourceConstants.MONEY, false);
        }


        public void AddView(MoneyView moneyView)
        {
            var model = new MoneyModel(moneyView, PResourceType.Soft, moneyView.MoneyCount);

            moneyDataList.Add(model);
        }

        public void CreateMoney(Transform position, int moneyGive)
        {
            var moneyPosition = position.position + Vector3.up * 0.4f;
            //var moneyView = PhotonNetwork.Instantiate(ResourceConstants.MONEY, moneyPosition, position.rotation);
            //moneyView.transform.position = position + Vector3.up * 0.4f;
            //moneyDataList.Add(new MoneyModel(moneyView.GetComponent<MoneyView>(), PResourceType.Soft, moneyGive));
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
            pickUpCoinHash["coinToDelete"] = data.View.Index;
            PhotonNetwork.LocalPlayer.SetCustomProperties(pickUpCoinHash);
            Object.Destroy(data.View.gameObject);
            data.ShowEffect();
            onMoneyPickupEvent?.Invoke((int) data.MoneyType, data.MoneyCount);

            moneyDataList.Remove(data);
        }

        public void DeleteModelByHash(int index)
        {
            var model = GetModelByIndex(index);
            Object.Destroy(model.View.gameObject);
            model.ShowEffect();
            moneyDataList.Remove(model);
        }

        public MoneyModel GetModelByIndex(int index)
        {
            foreach (var moneyModel in moneyDataList)
            {
                if (moneyModel.View.Index == index)
                {
                    return moneyModel;
                }
            }

            return default;
        }

        private void RotateMoney(MoneyModel moneyData, float dt)
        {
            moneyData.CachedTransform.Rotate(Vector3.up * (moneyData.View.RotationSpeed * dt), Space.Self);
        }

        public void GetMoneyInRadius(Vector3 position, float radius, ref List<MoneyModel> models)
        {
            int count = 0;
            for (int i = 0; i < moneyDataList.Count; i++)
            {
                var moneyModel = moneyDataList[i];
                var moneyPosition = moneyModel.CachedTransform.position;
                moneyPosition.y = position.y;
                if (Vector3.Distance(moneyPosition, position) < radius)
                {
                    models.Add(moneyModel);
                    count++;
                }
            }
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