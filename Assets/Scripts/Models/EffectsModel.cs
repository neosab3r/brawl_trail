using UnityEngine;

namespace OLS_HyperCasual
{
    public class EffectsModel : PoolableModel<EffectsView>
    {
        private float lifeTime;
        private float playbackTime;

        private Transform CachedTransform;
    
        public EffectsModel(EffectsView view)
        {
            View = view;
            playbackTime = view.PlaybackTime;
            CachedTransform = view.transform;
        }

        public void ShowEffect(Vector3 position)
        {
            CachedTransform.position = position;
            CachedTransform.gameObject.SetActive(true);
            lifeTime = 0;
        }
        
        public bool IsAlive()
        {
            return lifeTime < playbackTime;
        }

        public void UpdateLifeTime(float deltaTime)
        {
            lifeTime += deltaTime;

            if (lifeTime >= playbackTime)
            {
                View.gameObject.SetActive(false);
            }
        }
    }
}