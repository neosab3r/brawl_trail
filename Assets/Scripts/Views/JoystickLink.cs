using OLS_HyperCasual;
using UnityEngine;

public class JoystickLink : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;

    private void Start()
    {
        var entry = BaseEntryPoint.GetInstance();
        entry.SubscribeOnBaseControllersInit(() =>
        {
            var controller = entry.GetController<JoystickController>();
            controller.AddView(joystick);
        });
    }
}