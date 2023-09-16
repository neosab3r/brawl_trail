using OLS_HyperCasual;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : BaseMonoController<SpawnView, SpawnModel>
{
    public override SpawnModel AddView(SpawnView view)
    {
        var model = new SpawnModel(view);
        modelsList.Add(model);

        return model;
    }

    public SpawnView GetRandomSpawnPoint()
    {
        Debug.Log("Count spawns: " + modelsList.Count);
        var index = Random.Range(0, modelsList.Count - 1);
        return modelsList[index].View;
    }
}