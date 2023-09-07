using UnityEngine;

namespace OLS_HyperCasual
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Renderer renderer;
        [SerializeField] private ParticleSystem effect;

        public float RotationSpeed => rotationSpeed;
        public MoneyModel Data { get; private set; }

        public void InitData(MoneyModel data)
        {
            if (Data != null)
            {
                Debug.LogError("[MoneyView.InitData]: Data already inited");
                return;
            }

            Data = data;
        }

        public void HideAndPlayEffect()
        {
            renderer.enabled = false;
            effect.gameObject.SetActive(true);
            effect.Play();
        }
    }
}