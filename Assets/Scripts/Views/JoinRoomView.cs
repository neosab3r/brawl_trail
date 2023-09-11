using System;
using System.Collections.Generic;
using OLS_HyperCasual;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;

public class JoinRoomView: MonoBehaviourPunCallbacks
{
    public Action<List<RoomInfo>> onRoomListUpdate;
    public List<RoomInfo> roomList;
    private void Start()
    {
        var entry = BaseEntryPoint.GetInstance();
        entry.SubscribeOnBaseControllersInit(() =>
        {
            var controller = entry.GetController<JoinRoomController>();
            controller.AddView(this);
        });
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    /*public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        foreach (var room in roomList)
        {
            if (room.RemovedFromList)
            {
                this.roomList.Remove(room);
                continue;
            }

            this.roomList.Add(room);
        }
        onRoomListUpdate?.Invoke(this.roomList);
    }*/

    /*public void JoinRoom(string name)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.JoinRandomRoom();
            return;
        }
    }*/
}