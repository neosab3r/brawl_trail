using System;
using System.Collections.Generic;
using OLS_HyperCasual;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Serialization;

public class JoinRoomView: MonoBehaviourPunCallbacks
{
    public Action<List<RoomInfo>> onRoomListUpdate;
    private void Start()
    {
        var entry = BaseEntryPoint.GetInstance();
        entry.SubscribeOnBaseControllersInit(() =>
        {
            var controller = entry.GetController<JoinRoomController>();
            controller.AddView(this);
        });
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        onRoomListUpdate?.Invoke(roomList);
    }

    public void JoinRoom(string name)
    {
        PhotonNetwork.JoinRoom(name);
    }
}