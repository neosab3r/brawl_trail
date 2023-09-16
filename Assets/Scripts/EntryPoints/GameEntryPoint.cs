using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
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
        AddController(new SpawnController());
        AddController(new GameUIController());
        AddController(new ResourcesController());
        AddController(new PrefabsController());
        AddController(new TimeDelayController());
        AddController(new BulletController());
        AddController(new MoneyController());
        AddController(new JoystickController());
        AddController(new PlayerController());
        base.InitControllers();

        var connect = GameObject.Find("ConnectEntryPoint").GetComponent<ConnectEntryPoint>();
        connect.SubscribeOnBaseControllersInit(() =>
        {
            connect.AddEntryPoint(this);
            connect.GetController<PhotonController>().PhotonView.moneyController = GetController<MoneyController>();
        });
    }

    protected override void InitPostControllers()
    {
        Get<BulletController>().PostInit();
        base.InitPostControllers();
        StartCoroutine(SpawnPlayer());
    }

    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        GetInstance().SubscribeOnBaseControllersInit(() =>
        {
            var spawnController = GetController<SpawnController>();
            var playerController = Get<PlayerController>();
            playerController.InstantiatePlayer(spawnController.GetRandomSpawnPoint().transform);
        });
    }
}