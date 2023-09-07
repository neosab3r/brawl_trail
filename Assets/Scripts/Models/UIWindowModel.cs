using UnityEngine;

namespace OLS_HyperCasual
{
    public class UIWindowModel : BaseModel<UIWindowView>
    {
        public UiHeadModel Head { get; private set; }
        public bool IsShowing { get; private set; }
        public virtual uint WindowType => 0;

        public UIWindowModel(UIWindowView view, UiHeadModel headData)
        {
            View = view;
            Head = headData;
            view.InitData(this);
        }

        public virtual void Show()
        {
            View.OnWindowPreShow(this);
            View.gameObject.SetActive(true);
            IsShowing = true;
        }

        public void Close()
        {
            View.gameObject.SetActive(false);
            IsShowing = false;
        }

        public virtual void OnWindowClose()
        {
            BaseEntryPoint.Get<UIController>().CloseWindow(WindowType);
        }
    }
}