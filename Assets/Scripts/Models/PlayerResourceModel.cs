using System;
using UnityEngine;

namespace OLS_HyperCasual
{
    public class PlayerResourceModel : BaseModel<UIResourceView>
    {
        public int CurrentAmount { get; private set; }
        
        public int MaxAmount { get; private set; }

        public int ResourceType { get; private set; }

        /// <summary>
        /// First - previous value;
        /// Second - current value;
        /// </summary>
        public Action<int, int> OnResourceChanged;

        public PlayerResourceModel(int resourceType, int initial, int maxAmount = -1)
        {
            ResourceType = resourceType;
            MaxAmount = maxAmount;
            SetResourceCount(initial);
        }

        public void SetView(UIResourceView view)
        {
            View = view;
            SetResourceCount(CurrentAmount);
        }
        
        public static PlayerResourceModel operator +(PlayerResourceModel a, int b)
        {
            a.SetResourceCount(a.CurrentAmount + b);
            return a;
        }
        
        public static PlayerResourceModel operator -(PlayerResourceModel a, int b)
        {
            a.SetResourceCount(a.CurrentAmount - b);
            return a;
        }

        public void AddResource(int addCount)
        {
            SetResourceCount(CurrentAmount + addCount);
        }

        public bool HasEnoughResource(int count)
        {
            return CurrentAmount >= count;
        }

        public void SpendResource(int spendCount)
        {
            SetResourceCount(CurrentAmount - spendCount);
        }

        public void SetResourceMaxCount(int count)
        {
            MaxAmount = count;
            SetResourceCount(CurrentAmount);
        }

        public void SetResourceCount(int count)
        {
            OnResourceChanged?.Invoke(CurrentAmount, count);
            if (MaxAmount < 0)
            {
                CurrentAmount = count;
            }
            else
            {
                CurrentAmount = Mathf.Clamp(count, 0, MaxAmount);
            }

            View?.SetResourceCount(CurrentAmount, MaxAmount);
        }
    }
}