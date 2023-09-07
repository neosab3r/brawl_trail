using UnityEngine;

namespace OLS_HyperCasual
{
    public class UiHeadModel : BaseModel<UIHeadView>
    {
        public UiHeadModel(UIHeadView view)
        {
            this.View = view;
            view.InitData(this);
        }
        
        public void SetBackgroundActive(bool isActive)
        {
            View.SetBackgroundActive(isActive);
        }

        public void OnBackgroundClick()
        {
            var controller = BaseEntryPoint.Get<UIController>();
            var topWindowData = controller.GetTopWindow();
            topWindowData.OnWindowClose();
        }
    }
}