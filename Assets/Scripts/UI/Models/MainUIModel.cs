using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUIModel  
{
    private VisualElement root;
    private List<UIBaseButton<EMainUIButtonType>> uiButtons = new List<UIBaseButton<EMainUIButtonType>>();
    private List<UIContainer> uiContainers = new List<UIContainer>();
    public MainUIModel(VisualElement root)
    {
        this.root = root;
        var buttonsContainer = root.Q("ButtonsContainer");
        InitButton(buttonsContainer, "JoinButton", EMainUIButtonType.Join);
        InitButton(buttonsContainer, "CreateButton", EMainUIButtonType.Create);

        InitUIContainer("", EMainUIButtonType.Create);
        InitUIContainer("", EMainUIButtonType.Join);

        foreach (var container in uiContainers)
        {
            container.Hide();
        }
    }

    private void InitUIContainer(string name, EMainUIButtonType buttonType)
    {
        var containerRoot = root.Q(name);
        switch (buttonType)
        {
            case EMainUIButtonType.Create:
            {
                uiContainers.Add(new CreateRoomUIModel(containerRoot));
                break;
            }
            case EMainUIButtonType.Join:
            {
                uiContainers.Add(new JoinRoomUIModel(containerRoot));
                break;
            }
        }
    }

    private void InitButton(VisualElement buttonsContainer, string name, EMainUIButtonType buttonType)
    {
        var button = buttonsContainer.Q<Button>(name);
        var toolButtonElement = new UIBaseButton<EMainUIButtonType>(button, buttonType);
        toolButtonElement.Subscribe(OnButtonClick);
        
        uiButtons.Add(toolButtonElement);
    }

    private void OnButtonClick(EMainUIButtonType buttonType)
    {
        Debug.Log(buttonType);
        foreach (var container in uiContainers)
        {
            if (container.ToolType == buttonType && container.IsShowing == false)
            {
                container.Show();
            }
            else
            {
                container.Hide();
            }
        }
    }
}