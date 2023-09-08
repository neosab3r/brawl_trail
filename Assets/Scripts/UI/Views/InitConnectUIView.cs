using OLS_HyperCasual;
using UnityEngine;
using UnityEngine.UIElements;

public class InitConnectUIView : MonoBehaviour
{
    [SerializeField] private EConnectUIType uiType;
    [SerializeField] private UIDocument document;

    private void Start()
    {
        BaseEntryPoint.GetInstance().SubscribeOnBaseControllersInit(() =>
        {
            BaseEntryPoint.Get<ConnectUIController>().SetUiByType(uiType, document.rootVisualElement);
        });
    }
}