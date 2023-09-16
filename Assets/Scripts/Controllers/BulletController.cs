using OLS_HyperCasual;
using Photon.Pun;
using UnityEngine;

public class BulletController : BaseMonoController<BulletView, BulletModel>
{
    public override bool HasFixedUpdate => true;
    private TimeDelayController timeDelayController;
    private PrefabsController prefabsController;
    private PlayerController playerController;
    private FireButtonLink fireButton;
    private BulletView bulletPrefab;
    private bool isFire = false;
    private const float spawnBulletDelay = 1f;

    public BulletController()
    {
        timeDelayController = BaseEntryPoint.Get<TimeDelayController>();
        prefabsController = BaseEntryPoint.Get<PrefabsController>();
        bulletPrefab = prefabsController.GetPrefab<BulletView>(EBulletType.DefaultBullet, null, false);
    }

    public override void PostInit()
    {
        playerController = BaseEntryPoint.Get<PlayerController>();
    }

    public override BulletModel AddView(BulletView view)
    {
        var model = new BulletModel(view);

        modelsList.Add(model);
        return model;
    }

    public void InitFireButton(FireButtonLink button)
    {
        fireButton = button;
        fireButton.Subscribe(Fire);
    }

    public void Fire()
    {
        if (isFire == false)
        {
            var direction = playerController.LocalPlayer.cachedPointToShootTransform.forward;
            var position = playerController.LocalPlayer.cachedPointToShootTransform.position;
            var rotation = playerController.LocalPlayer.cachedPointToShootTransform.rotation;
            playerController.LocalPlayer.View.PhotonView.RPC("SpawnBulletWithDelay", RpcTarget.All,
                position, rotation,
                direction);
            isFire = true;
            timeDelayController.StartDelay(spawnBulletDelay, () => SetIsFireBool());
        }
    }

    public void SpawnBulletWithDelay(Vector3 position, Quaternion rotation, Vector3 direction, PhotonMessageInfo info)
    {
        float lag = (float) (PhotonNetwork.Time - info.SentServerTime);
        var bullet = Object.Instantiate(bulletPrefab, position, rotation);
        var model = AddView(bullet);
       
        model.ShowBullet(5, direction, Mathf.Abs(lag), playerController.LocalPlayer.View.PhotonView);
    }
    
    private void SetIsFireBool()
    {
        if (isFire)
        {
            isFire = false;
        }
    }

    private void DestroyModel(BulletModel model)
    {
        Object.Destroy(model.View.gameObject);
        modelsList.Remove(model);
    }
    
    public override void FixedUpdate(float dt)
    {
        for (int i = modelsList.Count - 1; i >= 0; i--)
        {
            var model = modelsList[i];
            if (model.IsUse == false)
            {
                DestroyModel(model);
            }
            model.UpdatePosition(dt);
        }
    }
}