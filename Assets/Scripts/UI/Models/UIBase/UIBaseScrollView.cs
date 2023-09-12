using UnityEngine.UIElements;

internal class UIBaseScrollView
{
    public ScrollView ScrollViewRoot;

    public UIBaseScrollView(ScrollView scrollView)
    {
        ScrollViewRoot = scrollView;
    }

    public void AddElement(VisualElement element)
    {
        ScrollViewRoot.contentContainer.Add(element);
    }

    public void ClearContentContainer()
    {
        ScrollViewRoot.contentContainer.Clear();
    }
}