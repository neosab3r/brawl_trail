using OLS_HyperCasual;
using UnityEngine;

public class PlayerModel : BaseModel<PlayerView>
{
    private Transform cachedTransform;
    private const float moveSpeed = 3f;
    private const float rotateSpeed = 35f;
    
    public PlayerModel(PlayerView view)
    {
        View = view;
        cachedTransform = View.PhotonView.transform;
    }

    public void Move(Vector3 direction, float dt)
    {
        cachedTransform.transform.position += cachedTransform.transform.forward * dt * moveSpeed;/*  new Vector3(direction.x * dt, 0, 0);*//*Translate(direction.x * 0.01f, direction.y * 0.01f, direction.z * 0.01f);*/
        cachedTransform.transform.rotation = Quaternion.Slerp(cachedTransform.transform.rotation, Quaternion.LookRotation(direction), dt * rotateSpeed);
    }
}