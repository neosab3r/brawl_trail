
namespace OLS_HyperCasual
{
    public abstract class BaseController : IController
    {
        public virtual bool HasUpdate => false;
        public virtual bool HasLateUpdate => false;
        public virtual bool HasFixedUpdate => false;

        public virtual void PostInit()
        {
            
        }
        
        public virtual void Update(float dt)
        {
            throw new System.NotImplementedException();
        }

        public virtual void LateUpdate(float dt)
        {
            throw new System.NotImplementedException();
        }

        public virtual void FixedUpdate(float dt)
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnApplicationPause()
        {
        }

        public virtual void OnDestroy()
        {
        }
    }
}