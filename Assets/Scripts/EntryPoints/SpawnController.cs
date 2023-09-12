using OLS_HyperCasual;
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
        var index = Random.Range(0, modelsList.Count);
        return modelsList[index].View;
    }
}