using OLS_HyperCasual;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class BulletModel : BaseModel<BulletView>
{
    public bool IsUse;
    public int WeaponDamage;
    public Transform CachedTransform;
    public PhotonView Player;
    private Vector3 direction;
    private float lag;

    private const float MoveSpeed = 15f;

    public BulletModel(BulletView view)
    {
        View = view;
        CachedTransform = View.transform;
        View.OnTrigger += HideBullet;
    }
    
    public void ShowBullet(int damage, Vector3 direction, float lag, PhotonView owner)
    {
        Player = owner;
        this.lag = lag; 
        this.direction = direction;
        View.gameObject.SetActive(true);
        View.ShowParticle();
        WeaponDamage = damage;
        IsUse = true;
    }

    public void HideBullet(PlayerView view)
    {
        this.View.OnTrigger -= HideBullet;
        if (view != null)
        {
            Player.RPC("TakeDamage", RpcTarget.All, view.PhotonView.ViewID, WeaponDamage);
        }
        View.gameObject.SetActive(false);
        IsUse = false;
    }
    
    public void UpdatePosition(float deltaTime)
    {
        CachedTransform.Translate(direction * (deltaTime * MoveSpeed), Space.World);
    }
}