using System;
using System.Collections.Generic;
using OLS_HyperCasual;
using UnityEngine.UIElements;

public class CreateRoomUIModel: UIContainer
{
    public override EMenuMainUIButtonType ToolType => EMenuMainUIButtonType.Create;
    private List<UIBaseButton<ECreateRoomUIButtonType>> uiButtons = new List<UIBaseButton<ECreateRoomUIButtonType>>();
    private List<UIBaseTextField> uiTextFields = new List<UIBaseTextField>();
    private CreateRoomController createRoomController;
    
    public CreateRoomUIModel(VisualElement root) : base(root)
    {
        //createRoomController = BaseEntryPoint.GetEntry<ConnectEntryPoint>().GetController<CreateRoomController>();
        createRoomController = BaseEntryPoint.Get<CreateRoomController>();
        var buttonBackContainer = root.Q("TopContainer");
        var buttonCreateContainer = root.Q("BottomContainer");
        var textFieldContainer = root.Q("MainContainer");

        InitTextField(textFieldContainer, "NameRoom");
        InitButton(buttonBackContainer, "BackButton", ECreateRoomUIButtonType.Back);
        InitButton(buttonCreateContainer, "CreateRoomButton", ECreateRoomUIButtonType.Create);
    }

    private void InitTextField(VisualElement container, string nameTextField)
    {
        var textField = container.Q<TextField>(nameTextField);
        var textFieldElement = new UIBaseTextField(textField);
        
        uiTextFields.Add(textFieldElement);
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
                createRoomController.View.CreateRoom(uiTextFields[0].GetTextFieldValue());
                
                Hide();
                break;
            }
        }
    }
}