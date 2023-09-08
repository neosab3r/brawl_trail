using System;
using OLS_HyperCasual;
using PTiles.Core.Scripts.Views.TilemapEditor.UI;
using UnityEngine.UIElements;


public class MenuUIController : BaseController
{
    private MenuMainUIModel mainModel;
    
    public void SetUiByType(EMenuUIType uiType, VisualElement root)
    {
        switch (uiType)
        {
            case EMenuUIType.MainUI:
                SetMainUIRoot(root);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(uiType), uiType, null);
        }
    }

    private void SetMainUIRoot(VisualElement root)
    {
        mainModel = new MenuMainUIModel(root);
    }
}