using UnityEngine.UIElements;

public class UIBaseTextLabel<T>
{
    public Label TextLabelRoot { get; }
    public T LabelType { get; }

    public UIBaseTextLabel(Label label, T type)
    {
        TextLabelRoot = label;
        LabelType = type;
    }

    public void SetTextValue(string text)
    {
        TextLabelRoot.text = text;
    }
}