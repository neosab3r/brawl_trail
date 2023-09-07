using System.Collections;
using UnityEngine;

namespace OLS_HyperCasual
{
    public class CoroutineController : BaseMonoController<CoroutineView, CoroutineModel>
    {
        public CoroutineController(int initialCount)
        {
            for (int i = 0; i < initialCount; i++)
            {
                AddCoroutineModel();
            }
        }
        
        public override CoroutineModel AddView(CoroutineView view)
        {
            var model = new CoroutineModel(view);
            modelsList.Add(model);
            return model;
        }
        
        public CoroutineModel AddCoroutine(IEnumerator enumerator)
        {
            var model = GetFreeModel();
            model.StartCoroutine(OnCompleteEnumerator(model, enumerator));
            return model;
        }

        public void RemoveCoroutine(CoroutineModel coroutine)
        {
            coroutine.StopCoroutine();
        }
        
        private CoroutineModel GetFreeModel()
        {
            foreach (var model in modelsList)
            {
                if (model.Coroutine != null)
                {
                    continue;
                }
                
                return model;
            }

            return AddCoroutineModel();
        }

        private CoroutineModel AddCoroutineModel()
        {
            var view = new GameObject("CoroutineView").AddComponent<CoroutineView>();
            return AddView(view);
        }

        private IEnumerator OnCompleteEnumerator(CoroutineModel model, IEnumerator enumerator)
        {
            yield return enumerator;
            model.OnComplete();
        }
    }
}