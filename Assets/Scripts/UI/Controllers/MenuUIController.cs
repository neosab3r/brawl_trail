using System;
using System.Collections.Generic;
using OLS_HyperCasual;
using PTiles.Core.Scripts.Views.TilemapEditor.UI;
using UnityEngine.UIElements;

public class MenuUIController : BaseController
{
    public MenuMainUIModel MainModel;
    public MenuRoomUIModel RoomModel;
    private List<UIContainer> containers = new List<UIContainer>();

    public Action OnShowRoomUI;
    
    public void SetUiByType(EMenuUIType uiType, VisualElement root)
    {
        switch (uiType)
        {
            case EMenuUIType.MainUI:
                SetMainUIRoot(root);
                break;
            case EMenuUIType.RoomUI:
                SetRoomUIRoot(root);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(uiType), uiType, null);
        }
    }

    private void SetRoomUIRoot(VisualElement root)
    {
        RoomModel = new MenuRoomUIModel(root);
        containers.Add(RoomModel);
    }

    private void SetMainUIRoot(VisualElement root)
    {
        MainModel = new MenuMainUIModel(root);
    }
}