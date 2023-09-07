using UnityEngine;

namespace OLS_HyperCasual
{
    public abstract class BaseModel<T>: IBaseModel where T : MonoBehaviour
    {
        public T View { get; protected set; }
    }
}