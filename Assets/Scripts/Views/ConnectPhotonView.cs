using System;
using System.Collections.Generic;
using OLS_HyperCasual;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ConnectPhotonView : MonoBehaviourPunCallbacks
{
    public ConnectEntryPoint EntryPoint;
    public Action<List<RoomInfo>> onRoomListUpdate;
    public List<RoomInfo> roomList = new List<RoomInfo>();
    private bool isFirstConnected;

    private void Start()
    {
        EntryPoint.SubscribeOnBaseControllersInit(() =>
        {
            EntryPoint.GetController<PhotonController>().AddView(this);
        });
        
        var gameSettingSO = EntryPoint.GetController<ResourcesController>()
            .GetResource<GameSettingsSO>(ResourceConstants.GameSettings);


        PhotonNetwork.NickName = gameSettingSO.GetValue("PlayerName") + Random.Range(0, 9999).ToString();
        PhotonNetwork.GameVersion = gameSettingSO.GetValue("GameVersion");
        PhotonNetwork.ConnectMethod = ConnectMethod.ConnectToMaster;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        isFirstConnected = true;
    }

    public override void OnConnected()
    {
        if (isFirstConnected)
        {
           SetConnectProgress(20);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect");
        Debug.Log("Name: " + PhotonNetwork.LocalPlayer.NickName);
        
        if (isFirstConnected)
        {
            SetConnectProgress(100);
        }
        
        if (PhotonNetwork.IsMasterClient == false)
        {
            Debug.Log("Client is not Master");
        }

        EntryPoint.entryPoints.Clear();
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined to Lobby");
        if (PhotonNetwork.IsMasterClient == true)
        {
            Debug.Log("Client is Master");
        }

        isFirstConnected = false;
    }
    
    public override void OnLeftLobby()
    {
        Debug.Log("LeftLobby " + this);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnect " + this);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        Debug.Log("Update roomList");
        this.roomList.Clear();

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
    }

    public void JoinRoom(string name)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.JoinRoom(name);
        }
    }

    public void SetConnectProgress(int value)
    {
        var entryPoint = EntryPoint.GetEntryPoint<LoadEntryPoint>();

        entryPoint.GetController<ConnectUIController>().SetProgressBarValue(value);
    }
}