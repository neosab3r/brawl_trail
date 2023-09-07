using System.Collections.Generic;
using UnityEngine;

namespace OLS_HyperCasual
{
    public class EffectsController : PoolableController<EffectsModel, EffectsView>
    {
        public override bool HasUpdate => true;
        
        public void ShowEffect(string effectType, Vector3 position)
        {
            var effect = GetFromPool(effectType);
            effect.ShowEffect(position);
        }
        
        public override void Update(float dt)
        {
            foreach (var kv in pooledModelsDict)
            {
                foreach (var kvModels in kv.Value)
                {
                    var model = kvModels.Value;
                    if (model.IsInPool)
                    {
                        continue;
                    }
                    
                    if (model.IsAlive())
                    {
                        model.UpdateLifeTime(dt);
                        continue;
                    }

                    ReturnToPool(model);
                }
            }
        }
    }
}