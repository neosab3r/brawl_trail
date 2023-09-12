using OLS_HyperCasual;
using Photon.Pun;
using UnityEngine;

public class PlayerController : BaseMonoController<PlayerView, PlayerModel>
{
    public override bool HasFixedUpdate => true;
    private PrefabsController prefabsController;
    private JoystickController joystickController;

    private bool isPlayerSpawned = false;

    public PlayerController()
    {
        joystickController = BaseEntryPoint.Get<JoystickController>();
        prefabsController = BaseEntryPoint.Get<PrefabsController>();
    }

    public override PlayerModel AddView(PlayerView view)
    {
        var model = new PlayerModel(view);

        modelsList.Add(model);

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
            if (playerModel.View.PhotonView.IsMine)
            {
                var vector2 = joystickController.View.Direction;
                Vector3 direction = Vector3.forward * vector2.y + Vector3.right * vector2.x;

                playerModel.Move(direction);
            }
        }
    }

    public void InstantiatePlayer(Transform transform)
    {
        //var playerView = prefabsController.Ge
        PhotonNetwork.Instantiate("Prefabs/Player/PlayerPrefab", transform.position, transform.rotation);
        isPlayerSpawned = true;
    }
}