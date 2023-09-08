using System.Collections;
using System.Collections.Generic;
using OLS_HyperCasual;
using UnityEngine;

public class ConnectEntryPoint : BaseEntryPoint
{
    protected override bool IsAllInited()
    {
        return true;
    }

    protected override void InitControllers()
    {
        AddController(new ConnectUIController());
        base.InitControllers();
    }
}