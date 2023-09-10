using System;
using OLS_HyperCasual;
using UnityEngine;

public class ConnectEntryPoint : BaseEntryPoint
{
    public void OnEnable()
    {
        //GameObject.DontDestroyOnLoad(this);
    }

    protected override bool IsAllInited()
    {
        return true;
    }

    protected override void InitControllers()
    {
        AddController(new ResourcesController());
        AddController(new PhotonController());
        //AddController(new CreateRoomController());
        base.InitControllers();
    }
}