using OLS_HyperCasual;
using UnityEngine;

public class PlayerModel : BaseModel<PlayerView>
{
    private Transform cachedTransform;
    
    public PlayerModel(PlayerView view)
    {
        View = view;
        cachedTransform = View.PhotonView.transform;
    }

    public void Move(Vector3 direction)
    {
        cachedTransform.transform.Translate(direction.x * 0.01f, direction.y * 0.01f, direction.z * 0.01f);
    }
}