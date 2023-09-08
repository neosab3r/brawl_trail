using System.Collections.Generic;
using UnityEngine.UIElements;

public class CreateRoomUIModel: UIContainer
{
    public override EMenuMainUIButtonType ToolType => EMenuMainUIButtonType.Create;
    private List<UIBaseButton<ECreateRoomUIButtonType>> uiButtons = new List<UIBaseButton<ECreateRoomUIButtonType>>();
    
    public CreateRoomUIModel(VisualElement root) : base(root)
    {
        var buttonBackContainer = root.Q("TopContainer");
        var buttonCreateContainer = root.Q("BottomContainer");

        InitButton(buttonBackContainer, "BackButton", ECreateRoomUIButtonType.Back);
        InitButton(buttonCreateContainer, "CreateRoomButton", ECreateRoomUIButtonType.Create);
    }

    private void InitButton(VisualElement container, string name, ECreateRoomUIButtonType buttonType)
    {
        var button = container.Q<Button>(name);
        var buttonElement = new UIBaseButton<ECreateRoomUIButtonType>(button, buttonType);
        buttonElement.Subscribe(OnButtonClick);
        
        uiButtons.Add(buttonElement);
    }
    
    private void OnButtonClick(ECreateRoomUIButtonType buttonType)
    {
        switch (buttonType)
        {
            case ECreateRoomUIButtonType.Back:
            {
                Hide();
                break;
            }
            case ECreateRoomUIButtonType.Create:
            {
                break;
            }
        }
    }
}