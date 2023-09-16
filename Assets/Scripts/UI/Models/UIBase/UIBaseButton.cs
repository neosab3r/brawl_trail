using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class UIBaseButton<T>
{
    public Button ButtonRoot { get; }
    public T ButtonType { get; }
    private Dictionary<Action<T>, Action> subscribedDict = new Dictionary<Action<T>, Action>();

    public UIBaseButton(Button root, T buttonType)
    {
        ButtonRoot = root;
        ButtonType = buttonType;
    }

    public void Subscribe(Action<T> onClickCallback)
    {
        var callback = new Action(() =>
        {
            onClickCallback?.Invoke(ButtonType);
        });
            
        subscribedDict.Add(onClickCallback, callback);
        ButtonRoot.clicked += callback;
    }

    public void SubscribeOnDown(Action<T> onClickCallback)
    {
        var callback = new Action(() =>
        {
            onClickCallback?.Invoke(ButtonType);
        });
            
        subscribedDict.Add(onClickCallback, callback);
        ButtonRoot.RegisterCallback<MouseDownEvent>(delegate { callback?.Invoke(); });
        //ButtonRoot.RegisterCallback<PointerDownEvent>(evt => callback?.Invoke());
    }

    public void Unsubscribe(Action<T> onClickCallback)
    {
        if (subscribedDict.TryGetValue(onClickCallback, out var callback) == false)
        {
            return;
        }

        ButtonRoot.clicked -= callback;
        subscribedDict.Remove(onClickCallback);
    }
}