using OLS_HyperCasual;
using UnityEngine.UIElements;

internal class ConnectMainUIModel
{
    private VisualElement root;
    private UIBaseProgressBar progressBar;
    
    public ConnectMainUIModel(VisualElement root)
    {
        this.root = root;
        var progressBarContainer = root.Q("ProgressBarContainer");
        InitProgressBar(progressBarContainer, "ConnectProgressBar");
    }

    private void InitProgressBar(VisualElement container, string name)
    {
        var progressBar = container.Q<ProgressBar>(name);
        var progressBarElement = new UIBaseProgressBar(progressBar);
        this.progressBar = progressBarElement;
    }

    public void SetProgressBarValue(int value)
    {
        progressBar.ChangeValue(value);
    }
}