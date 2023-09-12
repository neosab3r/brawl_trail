using System;
using System.Collections;
using OLS_HyperCasual;
using UnityEngine;

public class GameEntryPoint : BaseEntryPoint
{
    protected override bool IsAllInited()
    {
        return true;
    }

    protected override void InitControllers()
    {
        AddController(new GameUIController());
        AddController(new ResourcesController());
        AddController(new PrefabsController());
        AddController(new SpawnController());
        AddController(new JoystickController());
        AddController(new PlayerController());
        base.InitControllers();
    }

    protected override void InitPostControllers()
    {
        var entry = BaseEntryPoint.GetInstance();
        entry.SubscribeOnBaseControllersInit(() =>
        {
            StartCoroutine(WaitTestCoroutine(entry));
        });
    }

    private IEnumerator WaitTestCoroutine(BaseEntryPoint entry)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        var spawnController = entry.GetController<SpawnController>();
        var playerController = Get<PlayerController>();
        playerController.InstantiatePlayer(spawnController.GetRandomSpawnPoint().transform);
    }
}