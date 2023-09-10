using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using OLS_HyperCasual;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LoadEntryPoint : BaseEntryPoint
{
    protected override bool IsAllInited()
    {
        return true;
    }

    protected override void InitControllers()
    {
        AddController(new ConnectUIController());
        base.InitControllers();
        var connect = GameObject.Find("ConnectEntryPoint").GetComponent<ConnectEntryPoint>();
        connect.SubscribeOnBaseControllersInit(() =>
        {
            connect.AddEntryPoint(this);
        });
    }
}