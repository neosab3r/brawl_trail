using UnityEngine;

namespace OLS_HyperCasual
{
    public class MoneyModel : BaseModel<MoneyView>
    {
        public Transform CachedTransform { get; }
        public int MoneyType { get; }
        public int MoneyCount { get; }

        public MoneyModel(MoneyView view, int moneyType, int moneyCount)
        {
            View = view;
            CachedTransform = view.transform;
            MoneyType = moneyType;
            MoneyCount = moneyCount;
            view.InitData(this);
        }

        public void ShowEffect()
        {
            View.HideAndPlayEffect();
        }
    }
}