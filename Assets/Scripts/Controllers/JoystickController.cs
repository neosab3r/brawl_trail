using OLS_HyperCasual;

public class JoystickController : BaseController
{
    public FixedJoystick View;
    
    public JoystickController()
    {
        
    }

    public void AddView(FixedJoystick view)
    {
        View = view;
    }
    
}