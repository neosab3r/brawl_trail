using System;
using OLS_HyperCasual;
using Photon.Pun;

public class JoinRoomView: MonoBehaviourPunCallbacks
{
    private void Start()
    {
        var entry = BaseEntryPoint.GetInstance();
        entry.SubscribeOnBaseControllersInit(() =>
        {
            var controller = entry.GetController<JoinRoomController>();
            controller.AddView(this);
        });
    }
}