using System;
using System.Collections.Generic;
using OLS_HyperCasual;
using Photon.Pun;
using UnityEngine.UIElements;

internal class GameUIController: BaseController
{
    public InterfaceUIModel InterfaceUIModel;
    public void SetUiByType(EGameUIType uiType, VisualElement root)
    {
        switch (uiType)
        {
            case EGameUIType.Interface:
                SetInterfaceUIRoot(root);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(uiType), uiType, null);
        }
    }

    private void SetInterfaceUIRoot(VisualElement root)
    {
        InterfaceUIModel = new InterfaceUIModel(root);
    }
}

public class InterfaceUIModel
{
    private VisualElement root;
    private List<UIBaseButton<EGameInterfaceUIButtonType>> uiButtons = new List<UIBaseButton<EGameInterfaceUIButtonType>>();
    private List<UIContainer> uiContainers = new List<UIContainer>();

    public InterfaceUIModel(VisualElement root)
    {
        this.root = root;
        var buttonsContainer = root.Q("MainContainer");

        InitButton(buttonsContainer, "LeftGame", EGameInterfaceUIButtonType.LeftRoom);
        InitButton(buttonsContainer, "Fire", EGameInterfaceUIButtonType.Fire);
    }

    private void InitButton(VisualElement buttonsContainer, string name, EGameInterfaceUIButtonType buttonType)
    {
        var button = buttonsContainer.Q<Button>(name);
        var buttonElement = new UIBaseButton<EGameInterfaceUIButtonType>(button, buttonType);
        buttonElement.Subscribe(OnButtonClick);
        
        uiButtons.Add(buttonElement);
    }

    private void OnButtonClick(EGameInterfaceUIButtonType buttonType)
    {
        switch (buttonType)
        {
            case EGameInterfaceUIButtonType.LeftRoom:
            {
                break;
            }
            case EGameInterfaceUIButtonType.Fire:
            {
                
                break;
            }
        }
    }
}