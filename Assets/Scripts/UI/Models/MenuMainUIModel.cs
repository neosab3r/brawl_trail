using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuMainUIModel  
{
    private VisualElement root;
    private List<UIBaseButton<EMenuMainUIButtonType>> uiButtons = new List<UIBaseButton<EMenuMainUIButtonType>>();
    private List<UIContainer> uiContainers = new List<UIContainer>();
    public MenuMainUIModel(VisualElement root)
    {
        this.root = root;
        var buttonsContainer = root.Q("ButtonsContainer");
        InitButton(buttonsContainer, "JoinButton", EMenuMainUIButtonType.Join);
        InitButton(buttonsContainer, "CreateButton", EMenuMainUIButtonType.Create);

        InitUIContainer("UICreateRoom", EMenuMainUIButtonType.Create);
        //InitUIContainer("", EMainUIButtonType.Join);

        foreach (var container in uiContainers)
        {
            container.Hide();
        }
    }

    private void InitUIContainer(string name, EMenuMainUIButtonType buttonType)
    {
        var containerRoot = root.Q(name);
        switch (buttonType)
        {
            case EMenuMainUIButtonType.Create:
            {
                uiContainers.Add(new CreateRoomUIModel(containerRoot));
                break;
            }
            case EMenuMainUIButtonType.Join:
            {
                uiContainers.Add(new JoinRoomUIModel(containerRoot));
                break;
            }
        }
    }

    private void InitButton(VisualElement buttonsContainer, string name, EMenuMainUIButtonType buttonType)
    {
        var button = buttonsContainer.Q<Button>(name);
        var buttonElement = new UIBaseButton<EMenuMainUIButtonType>(button, buttonType);
        buttonElement.Subscribe(OnButtonClick);
        
        uiButtons.Add(buttonElement);
    }

    private void OnButtonClick(EMenuMainUIButtonType buttonType)
    {
        foreach (var container in uiContainers)
        {
            Debug.Log(container.ToolType + " -- " + buttonType + " " + this);
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