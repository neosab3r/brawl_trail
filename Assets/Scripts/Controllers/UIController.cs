using System.Collections.Generic;

namespace OLS_HyperCasual
{
    public class UIController : BaseMonoController<UIHeadView, UiHeadModel>
    {
        private List<UIWindowModel> windows = new List<UIWindowModel>();
        private List<UIWindowModel> layers = new List<UIWindowModel>();
        private FloatingJoystick joystick;

        public UIController(FloatingJoystick floatingJoystick = null)
        {
            joystick = floatingJoystick;
        }

        public override UiHeadModel AddView(UIHeadView view)
        {
            var headData = new UiHeadModel(view);
            modelsList.Add(headData);
            return headData;
        }

        public void AddWindow(UIWindowView windowView, UiHeadModel headData)
        {
            windows.Add(windowView.CreateWindowData(windowView, headData));
        }

        public T GetWindowData<T>(uint windowType) where T : UIWindowModel
        {
            foreach (var window in windows)
            {
                if (window.WindowType == windowType)
                {
                    return window as T;
                }
            }

            return null;
        }

        public UIWindowModel GetTopWindow()
        {
            return layers.Count == 0 ? null : layers[^1];
        }

        public void ShowWindow(uint windowType)
        {
            var window = GetWindowData<UIWindowModel>(windowType);
            if (window != null)
            {
                Internal_ShowWindow(window);
            }
        }

        public void CloseWindow(uint windowType)
        {
            var window = GetWindowData<UIWindowModel>(windowType);
            if (window != null)
            {
                Internal_CloseWindow(window);
            }
        }

        private void Internal_ShowWindow(UIWindowModel data)
        {
            if (data.IsShowing)
            {
                return;
            }

            if (layers.Count > 0)
            {
                layers[^1].Close();
            }

            if (joystick != null)
            {
                joystick.gameObject.SetActive(false);
            }

            layers.Add(data);
            data.Head.SetBackgroundActive(true);
            data.Show();
        }

        private void Internal_CloseWindow(UIWindowModel data)
        {
            if (data.IsShowing == false)
            {
                return;
            }

            data.Head.SetBackgroundActive(false);
            data.Close();
            layers.Remove(data);

            if (layers.Count > 0)
            {
                data.Head.SetBackgroundActive(true);
                layers[^1].Show();
            }
            else if (joystick != null)
            {
                joystick.gameObject.SetActive(true);
            }
        }
    }
}