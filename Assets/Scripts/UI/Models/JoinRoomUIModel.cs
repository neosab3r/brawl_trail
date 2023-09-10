using System.Collections.Generic;
using OLS_HyperCasual;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UIElements;

public class JoinRoomUIModel : UIContainer
{
    public override EMenuMainUIButtonType ToolType => EMenuMainUIButtonType.Join;

    private List<UIBaseButton<EJoinRoomUIButtonType>> uiButtons = new List<UIBaseButton<EJoinRoomUIButtonType>>();
    private List<UIBaseButton<string>> roomsContent = new List<UIBaseButton<string>>();
    private string currentRoomName = null;
    private UIBaseTemplateContainer contentElement;
    private UIBaseScrollView scrollViewElement;
    private JoinRoomController joinRoomController;
    private ResourcesController resourcesController;

    public JoinRoomUIModel(VisualElement root) : base(root)
    {
        joinRoomController = BaseEntryPoint.Get<JoinRoomController>();
        resourcesController = BaseEntryPoint.Get<ResourcesController>();

        var contentButton = resourcesController.GetResource<VisualTreeAsset>(ResourceConstants.JOINROOMCONTENTELEMENT);
        contentElement = new UIBaseTemplateContainer(contentButton.CloneTree());

        var buttonBackContainer = root.Q("TopContainer");
        var buttonJoinContainer = root.Q("BottomContainer");
        var scrollViewContainer = root.Q("CenterContainer");

        InitButton(buttonBackContainer, "BackButton", EJoinRoomUIButtonType.Back);
        InitButton(buttonJoinContainer, "JoinButton", EJoinRoomUIButtonType.Join);

        InitRoomScrollView(scrollViewContainer, "ScrollView");
    }

    public override void Show()
    {
        base.Show();
        joinRoomController.View.onRoomListUpdate += CreateRoomContent;
        
       Debug.Log("Count Room: " + PhotonNetwork.CountOfRooms);
    }

    public override void Hide()
    {
        if (joinRoomController.View != null)
        {
            joinRoomController.View.onRoomListUpdate -= CreateRoomContent;
        }

        base.Hide();
    }

    private void CreateRoomContent(List<RoomInfo> roomList)
    {
        scrollViewElement.ClearContentContainer();

        foreach (var roomInfo in roomList)
        {
            var visualElement = contentElement.TemplateContainerRoot.templateSource.CloneTree();
            var buttonElement = visualElement.Q<Button>("RoomContent");

            var roomElement = new UIBaseButton<string>(buttonElement, roomInfo.Name);
            roomElement.Subscribe(OnButtonContentClick);
            roomElement.ButtonRoot.text = roomInfo.Name;
            scrollViewElement.AddElement(visualElement);

            roomsContent.Add(roomElement);
        }
    }

    private void InitRoomScrollView(VisualElement container, string name)
    {
        var scrollView = container.Q<ScrollView>(name);
        var scrollViewElement = new UIBaseScrollView(scrollView);
        this.scrollViewElement = scrollViewElement;
    }

    private void InitButton(VisualElement container, string name, EJoinRoomUIButtonType buttonType)
    {
        var button = container.Q<Button>(name);
        var buttonElement = new UIBaseButton<EJoinRoomUIButtonType>(button, buttonType);
        buttonElement.Subscribe(OnButtonClick);

        uiButtons.Add(buttonElement);
    }

    private void OnButtonContentClick(string roomName)
    {
        currentRoomName = roomName;
    }

    private void OnButtonClick(EJoinRoomUIButtonType buttonType)
    {
        switch (buttonType)
        {
            case EJoinRoomUIButtonType.Back:
            {
                Hide();
                break;
            }
            case EJoinRoomUIButtonType.Join:
            {
                if (currentRoomName != null)
                {
                    joinRoomController.View.JoinRoom(currentRoomName);
                }

                break;
            }
        }
    }
}