using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace OLS_HyperCasual
{
    public class UIResourceView : MonoBehaviour
    {
        [SerializeField] private float animationDuration;
        [SerializeField] private int resourceType;
        [SerializeField] private TMP_Text resourceCountText;
        [SerializeField] private string prefixStr = "";
        [SerializeField] private string postfixStr = "";
        [SerializeField] private bool updateInEditor = true;

        private Tweener tweener;
        private int lastValue;

        protected virtual void Start()
        {
            var entry = BaseEntryPoint.GetInstance();
            entry.SubscribeOnBaseControllersInit(() =>
            {
                var controller = entry.GetController<PlayerResourcesController>();
                controller.SetResourceView(resourceType, this);
            });
        }

        protected virtual void OnValidate()
        {
            if (resourceCountText == null || updateInEditor == false)
            {
                return;
            }

            SetResourceCount(1, 2);
        }

        public virtual void SetResourceCount(int count, int maxCount)
        {
            if (tweener == null)
            {
                tweener = DOTween.To(() => lastValue, (value) =>
                {
                    lastValue = value;
                    resourceCountText.text = $"{prefixStr}{value}{postfixStr}";
                }, count, animationDuration).SetAutoKill(false).SetEase(Ease.Linear);
            }
            else
            {
                tweener.ChangeEndValue(count, animationDuration, true).Restart();
            }
        }
    }
}