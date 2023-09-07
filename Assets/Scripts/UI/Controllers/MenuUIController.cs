using System;
using OLS_HyperCasual;
using PTiles.Core.Scripts.Views.TilemapEditor.UI;
using UnityEngine.UIElements;


public class MenuUIController : BaseController
{
    private MainUIModel mainModel;
    
    public void SetUiByType(EGameUIType uiType, VisualElement root)
    {
        switch (uiType)
        {
            case EGameUIType.MainUI:
                SetMainUIRoot(root);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(uiType), uiType, null);
        }
    }

    private void SetMainUIRoot(VisualElement root)
    {
        mainModel = new MainUIModel(root);
    }
}