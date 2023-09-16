using System.Collections.Generic;
using NUnit.Framework.Internal.Execution;
using OLS_HyperCasual;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuRoomUIModel: UIContainer
{
    private List<UIBaseButton<EMenuRoomUIButtonType>> uiButtons = new List<UIBaseButton<EMenuRoomUIButtonType>>();
    private List<UIBaseTextLabel<EMenuRoomUILabelType>> uiLabels = new List<UIBaseTextLabel<EMenuRoomUILabelType>>();
    private List<UIBaseTextLabel<string>> scrollViewUILabels = new List<UIBaseTextLabel<string>>();
    private UIBaseScrollView scrollViewElement;

    private ResourcesController resourcesController;
    private UIBaseTemplateContainer contentElement;
    private UIBaseTemplateContainer contentElementMaster;

    public MenuRoomUIModel(VisualElement root) : base(root)
    {
        resourcesController = BaseEntryPoint.Get<ResourcesController>();
        
        var contentLabel = resourcesController.GetResource<VisualTreeAsset>(ResourceConstants.ROOMCONTENTELEMENT);
        var contentLabelMaster = resourcesController.GetResource<VisualTreeAsset>(ResourceConstants.ROOMCONTENTELEMENTCREATOR);
        contentElement = new UIBaseTemplateContainer(contentLabel.CloneTree());
        contentElementMaster = new UIBaseTemplateContainer(contentLabelMaster.CloneTree());

        var textLabelContainer = root.Q("MainContainer").Q("TopContainer");
        var buttonsContainer = root.Q("MainContainer").Q("BottomContainer");
        var scrollViewContainer = root.Q("MainContainer").Q("CenterContainer");

        InitTextLabel(textLabelContainer, "NameRoom", EMenuRoomUILabelType.NameRoom);
        InitTextLabel(textLabelContainer, "CountPlayers", EMenuRoomUILabelType.CountPlayers);
        
        InitButton(buttonsContainer, "LeftRoom", EMenuRoomUIButtonType.Back);
        InitButton(buttonsContainer, "PlayGame", EMenuRoomUIButtonType.Play);

        InitRoomScrollView(scrollViewContainer, "ScrollView");
    }

    private void InitTextLabel(VisualElement container, string name, EMenuRoomUILabelType labelType)
    {
        var label = container.Q<Label>(name);
        var labelElement = new UIBaseTextLabel<EMenuRoomUILabelType>(label, labelType);
        
        uiLabels.Add(labelElement);
    }


    private void InitRoomScrollView(VisualElement container, string name)
    {
        var scrollView = container.Q<ScrollView>(name);
        var scrollViewElement = new UIBaseScrollView(scrollView);
        this.scrollViewElement = scrollViewElement;
    }
    
    private void InitButton(VisualElement container, string name, EMenuRoomUIButtonType buttonType)
    {
        var button = container.Q<Button>(name);
        var buttonElement = new UIBaseButton<EMenuRoomUIButtonType>(button, buttonType);
        buttonElement.Subscribe(OnButtonClick);
        
        uiButtons.Add(buttonElement);
    }

    private void OnButtonClick(EMenuRoomUIButtonType buttonType)
    {
        switch (buttonType)
        {
            case EMenuRoomUIButtonType.Back:
            {
                this.Hide();
                PhotonNetwork.LeaveRoom();
                break;
            }
            case EMenuRoomUIButtonType.Play:
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    PhotonNetwork.CurrentRoom.IsVisible = false;
                    PhotonNetwork.CurrentRoom.IsOpen = false;
                    PhotonNetwork.LoadLevel("Game");
                }
                break;
            }
        }
    }

    private void ChangeLabelTextValue(EMenuRoomUILabelType type, string text)
    {
        foreach (var label in uiLabels)
        {
            if (label.LabelType == type)
            {
                label.SetTextValue(text);
                return;
            }
        }
    }

    public override void Show()
    {
        root.Q("MainContainer").style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        base.Show();
    }

    public override void Hide()
    {
        root.Q("MainContainer").style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        base.Hide();
    }

    public void ShowUI(string nameRoom, int countPlayers, Dictionary<int,Player> players, int masterRoom)
    {
        scrollViewElement.ClearContentContainer();
        ChangeLabelTextValue(EMenuRoomUILabelType.NameRoom, "Комната: " + nameRoom);
        ChangeLabelTextValue(EMenuRoomUILabelType.CountPlayers, countPlayers.ToString() + "/4");

        foreach (var player in players)
        {
            if (player.Key == masterRoom)
            {
                var visualElementMaster = contentElementMaster.TemplateContainerRoot.templateSource.CloneTree();
                var container = visualElementMaster.Q("NameContainer");
                var labelElementMaster = container.Q<Label>("PlayerNameText");
                var labelElementCreator = container.Q<Label>("CreatorText");
                labelElementCreator.text = "Creator Room";
                
                var PlayerNameLabelMaster = new UIBaseTextLabel<string>(labelElementMaster, player.Value.UserId);
                PlayerNameLabelMaster.SetTextValue(player.Value.NickName);
            
                scrollViewElement.AddElement(container);
            
                scrollViewUILabels.Add(PlayerNameLabelMaster);
                continue;
            }
            
            var visualElement = contentElement.TemplateContainerRoot.templateSource.CloneTree();
            var labelElement = visualElement.Q<Label>("PlayerNameText");

            var PlayerNameLabel = new UIBaseTextLabel<string>(labelElement, player.Value.UserId);
            PlayerNameLabel.SetTextValue(player.Value.NickName);
            
            scrollViewElement.AddElement(PlayerNameLabel.TextLabelRoot);
            
            scrollViewUILabels.Add(PlayerNameLabel);
        }
    }
}