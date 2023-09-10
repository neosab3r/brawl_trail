using System;
using OLS_HyperCasual;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class CreateRoomView : MonoBehaviourPunCallbacks
{    
    public ConnectEntryPoint EntryPoint;
    private string name;
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
        PhotonNetwork.CreateRoom(roomName, options, TypedLobby.Default);
        name = roomName;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Join in Room");
        PhotonNetwork.CurrentRoom.IsOpen = true;
        PhotonNetwork.CurrentRoom.IsVisible = true;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Create room failed: " + returnCode + " , " + message);
    }
}