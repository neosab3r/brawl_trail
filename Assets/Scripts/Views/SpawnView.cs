using OLS_HyperCasual;
using UnityEngine;

public class SpawnView : MonoBehaviour
{
    private void Start()
    {
        var entry = BaseEntryPoint.GetInstance();
        entry.SubscribeOnBaseControllersInit(() =>
        {
            var controller = entry.GetController<SpawnController>();
            controller.AddView(this);
        });
    }
}