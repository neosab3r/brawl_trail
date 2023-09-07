using OLS_HyperCasual;
using PTiles.Core.Scripts.Views.TilemapEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class InitUIView : MonoBehaviour
{
    [SerializeField] private EGameUIType uiType;
    [SerializeField] private UIDocument document;

    private void Start()
    {
        BaseEntryPoint.GetInstance().SubscribeOnBaseControllersInit(() =>
        {
            BaseEntryPoint.Get<MenuUIController>().SetUiByType(uiType, document.rootVisualElement);
        });
    }
}