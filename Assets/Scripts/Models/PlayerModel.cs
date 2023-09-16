using System.Collections.Generic;
using OLS_HyperCasual;
using Photon.Pun;
using UnityEngine;

public class PlayerModel : BaseModel<PlayerView>
{
    public Transform cachedPointToShootTransform;
    public bool isDeath = false;
    private Transform cachedTransform;
    private Animator cachedAnimator;
    private int PlayerHP;
    private List<int> ViewIDs = new List<int>();
    private const float moveSpeed = 3f;
    private const float rotateSpeed = 35f;

    public PlayerModel(PlayerView view)
    {
        PlayerHP = 10;
        View = view;
        View.SliderHP.maxValue = PlayerHP;
        View.SliderHP.value = PlayerHP;
        cachedTransform = View.PhotonView.transform;
        cachedAnimator = View.Animator;
        cachedPointToShootTransform = View.weaponPos;
        View.OnTakeDamage += ReduceHP;
        View.OnDeathPlayer += SetDeath;
    }

    public void Move(Vector3 direction, float dt)
    {
        cachedTransform.transform.position += cachedTransform.transform.forward * dt * moveSpeed;
        cachedTransform.transform.rotation = Quaternion.Slerp(cachedTransform.transform.rotation,
            Quaternion.LookRotation(direction), dt * rotateSpeed);
    }

    public void SetAnimationState(EPlayerAnimState state)
    {
        switch (state)
        {
            case EPlayerAnimState.Idle:
            {
                cachedAnimator.SetFloat("Run", 0);
                break;
            }
            case EPlayerAnimState.Run:
            {
                cachedAnimator.SetFloat("Run", 1);
                break;
            }
        }
    }
    

    public void ExitLobby()
    {
        View.PhotonView.RPC("Exit", RpcTarget.AllViaServer);
    }

    private void ReduceHP(int damage)
    {
        PlayerHP -= damage;
        if (PlayerHP <= 0)
        {
            PlayerHP = 0;
            View.meshRenderer.enabled = false;
            View.Collider.enabled = false;
            View.rg.isKinematic = true;
            View.SliderHP.gameObject.SetActive(false);
            if (View.PhotonView.IsMine)
            {
                View.PhotonView.RPC("IsDeath", RpcTarget.AllViaServer, true);
            }
        }

        View.SliderHP.value -= damage;
    }

    private void SetDeath(bool value)
    {
        isDeath = value;
    }
}