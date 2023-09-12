using System;
using System.Collections.Generic;
using OLS_HyperCasual;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class CreateRoomView : MonoBehaviourPunCallbacks
{
    private MenuUIController menuUIController;
    public void Start()
    {
        var entry = BaseEntryPoint.GetInstance();
        entry.SubscribeOnBaseControllersInit(() =>
        {
            var controller = entry.GetController<CreateRoomController>();
            controller.AddView(this);
            menuUIController = BaseEntryPoint.Get<MenuUIController>();
        });
    }

    private void GetRoomInfo()
    {
        var nameRoom = PhotonNetwork.CurrentRoom.Name;
        var countPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        var players = PhotonNetwork.CurrentRoom.Players;
        var masterRoom = PhotonNetwork.CurrentRoom.MasterClientId;
        menuUIController.RoomModel.Show();
        menuUIController.RoomModel.ShowUI(nameRoom, countPlayers, players, masterRoom);
    }

    public void CreateRoom(string roomName)
    {
        RoomOptions options = new RoomOptions();
        options.IsVisible = true;
        options.IsOpen = true;
        options.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(roomName, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Join in Room" + " || Client IsMaster: " + PhotonNetwork.IsMasterClient);
        GetRoomInfo();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left Room  " + this);
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }

        GetRoomInfo();
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
        
        GetRoomInfo();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Create room failed: " + returnCode + " , " + message);
    }
}