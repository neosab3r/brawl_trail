using OLS_HyperCasual;
using UnityEngine;
using UnityEngine.UIElements;

public class InitGameUIView : MonoBehaviour
{
    [SerializeField] private EGameUIType uiType;
    [SerializeField] private UIDocument document;

    private void Start()
    {
        BaseEntryPoint.GetInstance().SubscribeOnBaseControllersInit(() =>
        {
            BaseEntryPoint.Get<GameUIController>().SetUiByType(uiType, document.rootVisualElement);
        });
    }
}