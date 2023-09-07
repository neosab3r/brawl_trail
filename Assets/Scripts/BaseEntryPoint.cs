using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OLS_HyperCasual
{
    public class BaseEntryPoint: MonoBehaviour
    {
        private Action onBaseControllersInited, onAllSystemsLoaded;
        private bool isBaseControllersInited, isAllSystemsLoaded;
        protected List<IController> controllers = new List<IController>();
        
        private static BaseEntryPoint instance;

        protected void Awake()
        {
            instance = this;
        }

        private IEnumerator Start()
        {
            var wait = new WaitForSeconds(0.01f);
            while (true)
            {
                if (IsAllInited())
                {
                    InitControllers();
                    InitPostControllers();
                    break;
                }
                yield return wait;
            }
        }
        
        public static BaseEntryPoint GetInstance()
        {
            return instance;
        }
        
        public static T Get<T>() where T : IController
        {
            return GetInstance().GetController<T>();
        }
        
        public static IController Get(Type type)
        {
            return GetInstance().GetController(type);
        }

        protected virtual bool IsAllInited()
        {
            return true;
        }

        protected virtual void InitControllers()
        {
            isBaseControllersInited = true;
            onBaseControllersInited?.Invoke();
            onBaseControllersInited = null;
        }

        protected virtual void InitPostControllers()
        {
            onAllSystemsLoaded?.Invoke();
            onAllSystemsLoaded = null;
        }
        
        public void SubscribeOnBaseControllersInit(Action callback)
        {
            if (isBaseControllersInited)
            {
                callback?.Invoke();
                return;
            }

            onBaseControllersInited += callback;
        }
        
        public void SubscribeOnAllSystemLoaded(Action callback)
        {
            if (isAllSystemsLoaded)
            {
                callback?.Invoke();
                return;
            }
            
            onAllSystemsLoaded += callback;
        } 

        public T GetController<T>() where T : IController
        {
            for (int i = 0; i < controllers.Count; i++)
            {
                if (controllers[i] is T targetController)
                {
                    return targetController;
                }
            }

            return default;
        }
        
        public IController GetController(Type type)
        {
            for (int i = 0; i < controllers.Count; i++)
            {
                if (controllers[i].GetType() == type)
                {
                    return controllers[i];
                }
            }

            return default;
        }

        protected void AddController(IController controller)
        {
            controllers.Add(controller);
        }

        private void Update()
        {
            float dt = Time.deltaTime;
            for (int i = controllers.Count - 1; i >= 0; i--)
            {
                var controller = controllers[i];
                if (controller.HasUpdate)
                {
                    controller.Update(dt);
                }
            }
        }

        private void LateUpdate()
        {
            float dt = Time.deltaTime;
            for (int i = controllers.Count - 1; i >= 0; i--)
            {
                var controller = controllers[i];
                if (controller.HasLateUpdate)
                {
                    controller.LateUpdate(dt);
                }
            }
        }

        private void FixedUpdate()
        {
            float dt = Time.fixedDeltaTime;
            for (int i = controllers.Count - 1; i >= 0; i--)
            {
                var controller = controllers[i];
                if (controller.HasFixedUpdate)
                {
                    controller.FixedUpdate(dt);
                }
            }
        }
        
        private void OnApplicationPause(bool pauseStatus)
        {
            if (instance == this && pauseStatus)
            {
                foreach (var controller in controllers)
                {
                    controller.OnApplicationPause();
                }
            }
        }

        private void OnDestroy()
        {
            DestroyAll();
        }

        private void DestroyAll()
        {
            if (instance == this)
            {
                foreach (var controller in controllers)
                {
                    controller.OnDestroy();
                }

                instance = null;
            }
        }
    }
}