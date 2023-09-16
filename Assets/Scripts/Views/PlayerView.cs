using System;
using OLS_HyperCasual;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    public Action<int> OnTakeDamage;
    public Action<bool> OnDeathPlayer;
    public PhotonView PhotonView;
    public PhotonAnimatorView PhotonAnimatorView;
    public Animator Animator;
    public Transform weaponPos;
    public Slider SliderHP;
    public BoxCollider Collider;
    public Renderer meshRenderer;
    public Rigidbody rg;
    private BulletController bulletController;
    private GameUIController gameUIController;
    private Camera gameCamera;

    private void Start()
    {
        gameCamera = Camera.main;
        var entry = BaseEntryPoint.GetInstance();
        entry.SubscribeOnBaseControllersInit(() =>
        {
            var controller = entry.GetController<PlayerController>();
            controller.AddView(this);
            bulletController = entry.GetController<BulletController>();
        });
    }
    

    [PunRPC]
    private void Exit()
    {
        if (PhotonView.IsMine)
        {
            PhotonNetwork.LoadLevel("Menu");
            PhotonNetwork.LeaveRoom();
        }
    }

    [PunRPC]
    private void IsDeath(bool value)
    {
        OnDeathPlayer?.Invoke(value);
    }

    [PunRPC]
    private void SpawnBulletWithDelay(Vector3 position, Quaternion rotation, Vector3 direction, PhotonMessageInfo info)
    {
        bulletController.SpawnBulletWithDelay(position, rotation, direction, info);
    }

    [PunRPC]
    private void TakeDamage(int ViewID, int damage)
    {
        if (PhotonView.ViewID == ViewID)
        {
            OnTakeDamage?.Invoke(damage);
        }
    }

    private void Update()
    {
        SliderHP.transform.LookAt(gameCamera.transform);
    }

    public void OnVictory()
    {
        var entry = BaseEntryPoint.GetInstance();
        gameUIController = entry.GetController<GameUIController>();
        PhotonView.RPC("ShowVictoryUI", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, gameUIController.InterfaceUIModel.CountMoney);
    }

    [PunRPC]
    public void ShowVictoryUI(string PlayerName, int CountMoney)
    {
        var entry = BaseEntryPoint.GetInstance();
        gameUIController = entry.GetController<GameUIController>();
        gameUIController.EndGameUIModel.SetTextInLabelByType(EGameEndUITextLabel.CountMoney, "Монет: " + CountMoney.ToString());
        gameUIController.EndGameUIModel.SetTextInLabelByType(EGameEndUITextLabel.PlayerName, "Игрок: " + PlayerName);
        gameUIController.EndGameUIModel.ShowUI();
    }
}