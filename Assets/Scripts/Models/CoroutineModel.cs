using System.Collections;
using UnityEngine;

namespace OLS_HyperCasual
{
    public class CoroutineModel : BaseModel<CoroutineView>
    {
        public Coroutine Coroutine { get; private set; }

        public CoroutineModel(CoroutineView view)
        {
            View = view;
        }

        public void StartCoroutine(IEnumerator coroutine)
        {
            if (Coroutine != null)
            {
                return;
            }
            
            Coroutine = View.StartCoroutine(coroutine);
        }

        public void StopCoroutine()
        {
            if (Coroutine == null)
            {
                return;
            }
            
            View.StopCoroutine(Coroutine);
            Coroutine = null;
        }

        public void OnComplete()
        {
            Coroutine = null;
        }
    }
}