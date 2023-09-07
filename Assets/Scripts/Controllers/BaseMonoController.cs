using System.Collections.Generic;
using UnityEngine;

namespace OLS_HyperCasual
{
    public abstract class BaseMonoController<T, TK> : BaseController where T:MonoBehaviour where TK:IBaseModel
    {
        protected readonly List<TK> modelsList = new List<TK>();

        public abstract TK AddView(T view);

        public virtual void RemoveModel(TK model)
        {
            modelsList.Remove(model);
        }
    }
}