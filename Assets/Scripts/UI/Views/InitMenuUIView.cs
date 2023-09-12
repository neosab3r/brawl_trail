using OLS_HyperCasual;
using PTiles.Core.Scripts.Views.TilemapEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class InitMenuUIView : MonoBehaviour
{
    [SerializeField] private EMenuUIType uiType;
    [SerializeField] private UIDocument document;

    private void Start()
    {
        BaseEntryPoint.GetInstance().SubscribeOnBaseControllersInit(() =>
        {
            BaseEntryPoint.Get<MenuUIController>().SetUiByType(uiType, document.rootVisualElement);
        });
    }
}