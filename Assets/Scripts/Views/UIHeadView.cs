using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OLS_HyperCasual;

namespace OLS_HyperCasual
{
    public class UIHeadView : MonoBehaviour
    {
        [SerializeField] private Button UiBackground;
        [SerializeField] private UIWindowView[] childWindows;

        public UiHeadModel Data { get; private set; }

        private void OnValidate()
        {
            childWindows = transform.GetComponentsInChildren<UIWindowView>(true);
        }

        private void Start()
        {
            var entry = BaseEntryPoint.GetInstance();
            entry.SubscribeOnBaseControllersInit(() =>
            {
                var controller = entry.GetController<UIController>();
                var headData = controller.AddView(this);

                foreach (var window in childWindows)
                {
                    controller.AddWindow(window, headData);
                }
            });
        }

        public void InitData(UiHeadModel data)
        {
            if (Data != null)
            {
                Debug.LogError("[UIHeadView.InitData]: Data already inited");
                return;
            }

            Data = data;
            UiBackground.onClick.AddListener(data.OnBackgroundClick);
        }

        public void SetBackgroundActive(bool isActive)
        {
            UiBackground.gameObject.SetActive(isActive);
        }
    }
}