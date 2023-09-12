using UnityEngine.UIElements;

public abstract class UIContainer
{
    public virtual EMenuMainUIButtonType ToolType { get; }
    public bool IsShowing { get; private set; }

    protected VisualElement root;

    public UIContainer(VisualElement root)
    {
        this.root = root;
    }

    public virtual void Show()
    {
        root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        IsShowing = true;
    }

    public virtual void Hide()
    {
        root.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        IsShowing = false;
    }
}