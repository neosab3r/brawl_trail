using OLS_HyperCasual;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class ConnectPhotonView : MonoBehaviourPunCallbacks
{
    public ConnectEntryPoint EntryPoint;
    private void Start()
    {
        EntryPoint.SubscribeOnBaseControllersInit(() => { EntryPoint.GetController<PhotonController>().AddView(this); });
        var gameSettingSO = EntryPoint.GetController<ResourcesController>()
            .GetResource<GameSettingsSO>(ResourceConstants.GameSettings);
        
        PhotonNetwork.NickName = gameSettingSO.GetValue("PlayerName");
        PhotonNetwork.GameVersion = gameSettingSO.GetValue("GameVersion");
        PhotonNetwork.ConnectUsingSettings();
        
    }

    public override void OnConnected()
    {
        SetConnectProgress(20);
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect");
        Debug.Log("Name: " + PhotonNetwork.LocalPlayer.NickName);
        SetConnectProgress(100);
        EntryPoint.entryPoints.Clear();
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnect " + this);
    }

    public void SetConnectProgress(int value)
    {
       var entryPoint = EntryPoint.GetEntryPoint<LoadEntryPoint>();
       
       entryPoint.GetController<ConnectUIController>().SetProgressBarValue(value);
    }
}