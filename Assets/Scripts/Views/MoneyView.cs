using System;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

namespace OLS_HyperCasual
{
    public class MoneyView : MonoBehaviour
    {
        public int MoneyCount = 1;
        public int Index;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Renderer renderer;

        private void Start()
        {
            var entry = BaseEntryPoint.GetInstance();
            entry.SubscribeOnBaseControllersInit(() =>
            {
                var controller = entry.GetController<MoneyController>();
                controller.AddView(this);
            });
        }

        public float RotationSpeed => rotationSpeed;
        public MoneyModel Data { get; private set; }

        public void InitData(MoneyModel data)
        {
            if (Data != null)
            {
                Debug.LogError("[MoneyView.InitData]: Data already inited");
                return;
            }

            Data = data;
        }

        public void Hide()
        {
            renderer.enabled = false;
            //effect.gameObject.SetActive(true);
            //effect.Play();
        }
    }
}