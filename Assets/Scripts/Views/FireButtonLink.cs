using System;
using OLS_HyperCasual;
using UnityEngine;

public class FireButtonLink : MonoBehaviour
{
    [SerializeField] private ButtonPointer button;

    private void Start()
    {
        var entry = BaseEntryPoint.GetInstance();
        entry.SubscribeOnBaseControllersInit(() =>
        {
            var controller = entry.GetController<BulletController>();
            controller.InitFireButton(this);
        });
    }

    public void Subscribe(Action act)
    {
        button.OnButtonDown += act;
    }
}