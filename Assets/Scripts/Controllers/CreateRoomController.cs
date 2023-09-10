using OLS_HyperCasual;

public class CreateRoomController : BaseController
{
    public CreateRoomView View;
    
    public void AddView(CreateRoomView roomView)
    {
        View = roomView;
    }
}