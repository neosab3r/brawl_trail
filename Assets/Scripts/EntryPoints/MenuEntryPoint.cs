using System.Collections;
using System.Collections.Generic;
using OLS_HyperCasual;
using UnityEngine;

public class MenuEntryPoint : BaseEntryPoint
{
    protected override bool IsAllInited()
    {
        return true;
    }

    protected override void InitControllers()
    {
        /*var connect = GameObject.Find("ConnectEntryPoint").GetComponent<ConnectEntryPoint>();
        connect.SubscribeOnBaseControllersInit(() =>
        {
            AddEntryPoint(connect);
            Debug.Log("Add");
        });*/
        AddController(new ResourcesController());
        AddController(new MenuUIController());
        AddController(new CreateRoomController());
        AddController(new JoinRoomController());
        base.InitControllers();
    }
    
}