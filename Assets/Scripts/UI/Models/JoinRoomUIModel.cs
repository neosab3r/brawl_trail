using System.Collections.Generic;
using UnityEngine.UIElements;

public class JoinRoomUIModel: UIContainer
{
    private List<UIBaseButton<EJoinRoomUIButtonType>> uiButtons = new List<UIBaseButton<EJoinRoomUIButtonType>>();
    
    public JoinRoomUIModel(VisualElement root) : base(root)
    {
        
    }
}