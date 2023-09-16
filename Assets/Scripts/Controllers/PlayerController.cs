using System;
using System.Collections.Generic;
using OLS_HyperCasual;
using Photon.Pun;
using UnityEngine;

public class PlayerController : BaseMonoController<PlayerView, PlayerModel>
{
    public override bool HasFixedUpdate => true;
    public PlayerModel LocalPlayer;
    private PrefabsController prefabsController;
    private JoystickController joystickController;
    private MoneyController moneyController;
    private GameUIController gameUIController;
    private bool isPlayerSpawned = false;
    private bool isEndGame = false;

    public PlayerController()
    {
        joystickController = BaseEntryPoint.Get<JoystickController>();
        prefabsController = BaseEntryPoint.Get<PrefabsController>();
        moneyController = BaseEntryPoint.Get<MoneyController>();
        gameUIController = BaseEntryPoint.Get<GameUIController>();
    }

    public override PlayerModel AddView(PlayerView view)
    {
        var model = new PlayerModel(view);

        modelsList.Add(model);

        if (view.PhotonView.IsMine)
        {
            LocalPlayer = model;
            gameUIController.EndGameUIModel.onExitGame += model.ExitLobby;
        }

        return model;
    }

    public override void FixedUpdate(float dt)
    {
        if (isPlayerSpawned == false)
        {
            return;
        }

        foreach (var playerModel in modelsList)
        {
            if (playerModel.View.PhotonView.IsMine && playerModel.isDeath == false)
            {
                List<MoneyModel> moneyModels = new List<MoneyModel>();
                moneyController.GetMoneyInRadius(playerModel.View.transform.position, 2f, ref moneyModels);

                for (int i = 0; i < moneyModels.Count; i++)
                {
                    moneyController.PickupMoney(moneyModels[i]);
                }

                var vector2 = joystickController.View.Direction;
                Vector3 direction = Vector3.forward * vector2.y + Vector3.right * vector2.x;

                if (direction != Vector3.zero)
                {
                    playerModel.Move(direction, dt);
                    playerModel.SetAnimationState(EPlayerAnimState.Run);
                }
                else
                {
                    playerModel.SetAnimationState(EPlayerAnimState.Idle);
                }
            }
        }

        if (NotMainViewsDeath() && isEndGame == false && modelsList.Count == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            LocalPlayer.View.OnVictory();
            isEndGame = true;
        }
    }

    private bool NotMainViewsDeath()
    {
        var models = GetNotMineModels();

        foreach (var model in models)
        {
            if (model.isDeath == false)
            {
                return false;
            }
        }

        return true;
    }

    public List<PlayerModel> GetNotMineModels()
    {
        List<PlayerModel> models = new List<PlayerModel>();
        foreach (var playerModel in modelsList)
        {
            if (playerModel.View.PhotonView.IsMine == false)
            {
                models.Add(playerModel);
            }
        }

        return models;
    }

    public PlayerModel GetMineModel()
    {
        foreach (var playerModel in modelsList)
        {
            if (playerModel.View.PhotonView.IsMine)
            {
                return playerModel;
            }
        }

        return default;
    }
    
    public void InstantiatePlayer(Transform transform)
    {
        PhotonNetwork.Instantiate("Prefabs/Player/PlayerPrefab", transform.position, transform.rotation);
        isPlayerSpawned = true;
    }
}