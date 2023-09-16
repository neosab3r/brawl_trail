using System;
using OLS_HyperCasual;
using UnityEngine.UIElements;

internal class GameUIController: BaseController
{
    public InterfaceUIModel InterfaceUIModel;
    public EndGameUIModel EndGameUIModel;
    
    public void SetUiByType(EGameUIType uiType, VisualElement root)
    {
        switch (uiType)
        {
            case EGameUIType.Interface:
                SetInterfaceUIRoot(root);
                break;
            case EGameUIType.End:
                SetEndGameUIRoot(root);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(uiType), uiType, null);
        }
    }

    private void SetEndGameUIRoot(VisualElement root)
    {
        EndGameUIModel = new EndGameUIModel(root);
    }

    private void SetInterfaceUIRoot(VisualElement root)
    {
        InterfaceUIModel = new InterfaceUIModel(root);
    }
}