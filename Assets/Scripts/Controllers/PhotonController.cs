using OLS_HyperCasual;

public class PhotonController : BaseController
{
    public ConnectPhotonView PhotonView;
    
    public void AddView(ConnectPhotonView connectPhotonView)
    {
        PhotonView = connectPhotonView;
    }
}