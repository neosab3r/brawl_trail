using System;
using System.Collections.Generic;
using OLS_HyperCasual;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InterfaceUIModel
{
    public int CountMoney = 0;
    private VisualElement root;
    private List<UIBaseButton<EGameInterfaceUIButtonType>> uiButtons = new List<UIBaseButton<EGameInterfaceUIButtonType>>();
    private List<UIContainer> uiContainers = new List<UIContainer>();
    private UIBaseTextLabel<string> countMoneyLabel;
    private MoneyController moneyController;

    public InterfaceUIModel(VisualElement root)
    {
        moneyController = BaseEntryPoint.Get<MoneyController>();
        MoneyController.OnMoneyPickup onMoneyPickup = (type, count) => OnMoneyPickup(type, count);
        moneyController.SubscribePickup(onMoneyPickup);
        this.root = root;
        var buttonsContainer = root.Q("MainContainer");
        var textLabelContainer = root.Q("MainContainer");

        InitTextLabel(textLabelContainer, "CountMoney");
        InitButton(buttonsContainer, "LeftGame", EGameInterfaceUIButtonType.LeftRoom);
    }

    private void OnMoneyPickup(int type, int count)
    {
        CountMoney += count;
        countMoneyLabel.SetTextValue("Монет: " + CountMoney.ToString());
    }

    private void InitTextLabel(VisualElement textLabelContainer, string name)
    {
        var textLabel = textLabelContainer.Q<Label>(name);
        var textLabelElement = new UIBaseTextLabel<string>(textLabel, name);

        countMoneyLabel = textLabelElement;
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
                SceneManager.LoadScene("Menu");
                PhotonNetwork.LeaveRoom();
                break;
            }
        }
    }
}