namespace OLS_HyperCasual
{
    public interface IController
    {
        public bool HasUpdate { get; }
        public bool HasLateUpdate { get; }
        public bool HasFixedUpdate { get; }

        public void Update(float dt);
        public void LateUpdate(float dt);
        public void FixedUpdate(float dt);

        public void OnApplicationPause();
        public void OnDestroy();
    }
}