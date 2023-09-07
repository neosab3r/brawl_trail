using System;
using System.Collections.Generic;
using UnityEngine;

namespace OLS_HyperCasual
{
    public class MovePointsCollectorView : MonoBehaviour
    {
        [field: SerializeField] public MovePointView[] PointViews { get; private set; }

        private void Start()
        {
            var entry = BaseEntryPoint.GetInstance();
            entry.SubscribeOnBaseControllersInit(() =>
            {
                var controller = entry.GetController<PathController>();
                controller.AddMovePointCollector(this);
            });
        }

        private void OnValidate()
        {
            PointViews = transform.GetComponentsInChildren<MovePointView>(true);
        }
    }
}