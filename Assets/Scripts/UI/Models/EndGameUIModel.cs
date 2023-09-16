using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndGameUIModel
{
    public Action onExitGame;
    private VisualElement root;
    private List<UIBaseButton<EGameEndUIButton>> uiButtons = new List<UIBaseButton<EGameEndUIButton>>();
    private List<UIBaseTextLabel<EGameEndUITextLabel>> uiLabels = new List<UIBaseTextLabel<EGameEndUITextLabel>>();
    
    public EndGameUIModel(VisualElement root)
    {
        this.root = root;
        var buttonContainer = root.Q("BottomContainer");
        var labelContainer = root.Q("VictoryContainer");

        InitButton(buttonContainer, "InLobby", EGameEndUIButton.Lobby);
        
        InitTextLabel(labelContainer, "Player", EGameEndUITextLabel.PlayerName);
        InitTextLabel(labelContainer, "CountMoney", EGameEndUITextLabel.CountMoney);
    }

    public void ShowUI()
    {
        root.Q("MainContainer").style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
    }

    public void SetTextInLabelByType(EGameEndUITextLabel type, string text)
    {
        foreach (var label in uiLabels)
        {
            if (label.LabelType == type)
            {
                label.SetTextValue(text);
            }
        }
    }

    private void InitButton(VisualElement buttonContainer, string name, EGameEndUIButton typeButton)
    {
        var button = buttonContainer.Q<Button>(name);
        var buttonElement = new UIBaseButton<EGameEndUIButton>(button, typeButton);
        buttonElement.Subscribe(OnButtonClick);
        uiButtons.Add(buttonElement);
    }

    private void OnButtonClick(EGameEndUIButton type)
    {
        switch (type)
        {
            case EGameEndUIButton.Lobby:
            {
                onExitGame?.Invoke();
                SceneManager.LoadScene("Menu");
                break;
            }
        }
    }

    private void InitTextLabel(VisualElement textLabelContainer, string name, EGameEndUITextLabel labelType)
    {
        var textLabel = textLabelContainer.Q<Label>(name);
        var textLabelElement = new UIBaseTextLabel<EGameEndUITextLabel>(textLabel, labelType);

        uiLabels.Add(textLabelElement);
    }
}