using UnityEngine.UIElements;

public class UIBaseProgressBar
{
    public ProgressBar ProgressBarRoot { get; }
    public UIBaseProgressBar(ProgressBar root)
    {
        ProgressBarRoot = root;
    }

    public void ChangeValue(int value)
    {
        ProgressBarRoot.value = value;
    }
}