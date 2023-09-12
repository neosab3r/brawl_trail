using System;
using OLS_HyperCasual;
using PTiles.Core.Scripts.Views.TilemapEditor.UI;
using UnityEngine.UIElements;

public class ConnectUIController : BaseController
{
    private ConnectMainUIModel mainUIModel;
    
    public void SetUiByType(EConnectUIType uiType, VisualElement root)
    {
        switch (uiType)
        {
            case EConnectUIType.MainUI:
                SetMainUIRoot(root);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(uiType), uiType, null);
        }
    }

    private void SetMainUIRoot(VisualElement root)
    {
        mainUIModel = new ConnectMainUIModel(root);
    }

    public void SetProgressBarValue(int value)
    {
        mainUIModel.SetProgressBarValue(value);
    }
}