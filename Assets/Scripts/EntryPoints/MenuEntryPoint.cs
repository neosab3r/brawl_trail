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
        AddController(new MenuUIController());
        base.InitControllers();
    }
    
}
