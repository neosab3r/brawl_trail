using System;
using OLS_HyperCasual;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class CreateRoomView : MonoBehaviourPunCallbacks
{    
    public ConnectEntryPoint EntryPoint;
    public void Start()
    {
        //EntryPoint.SubscribeOnBaseControllersInit(() => { EntryPoint.GetController<CreateRoomController>().AddView(this); });
        var entry = BaseEntryPoint.GetInstance();
        entry.SubscribeOnBaseControllersInit(() =>
        {
            var controller = entry.GetController<CreateRoomController>();
            controller.AddView(this);
        });
    }

    public void CreateRoom(string roomName)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(roomName, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Create room failed: " + returnCode + " , " + message);
    }
}