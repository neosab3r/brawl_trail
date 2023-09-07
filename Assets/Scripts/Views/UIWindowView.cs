using System;
using UnityEngine;
using UnityEngine.UI;

namespace OLS_HyperCasual
{
    public class UIWindowView : MonoBehaviour
    {
        [SerializeField] private Button closeButton;

        public UIWindowModel Data { get; private set; }

        public virtual void InitData(UIWindowModel data)
        {
            if (Data != null)
            {
                Debug.LogError("[UIWindowView.InitData]: Data already inited");
                return;
            }

            Data = data;
            if (closeButton)
            {
                closeButton.onClick.AddListener(data.OnWindowClose);
            }
        }

        public virtual UIWindowModel CreateWindowData(UIWindowView windowView, UiHeadModel headData)
        {
            return new UIWindowModel(windowView, headData);
        }

        public virtual void OnWindowPreShow(UIWindowModel windowData)
        {
        }
    }
}