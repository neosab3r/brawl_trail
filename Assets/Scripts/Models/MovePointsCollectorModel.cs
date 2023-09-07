using System.Collections.Generic;
using UnityEngine;

namespace OLS_HyperCasual
{
    public class MovePointsCollectorModel : BaseModel<MovePointsCollectorView>
    {
        public IReadOnlyList<MovePointModel> AllPointsList { get; }

        public MovePointsCollectorModel(MovePointsCollectorView view, List<MovePointModel> allPoints)
        {
            View = view;
            AllPointsList = allPoints;
        }

        public MovePointModel GetNearestGlobalPoint(Vector3 position)
        {
            MovePointModel nearestPoint = null;
            float minimumDistance = float.MaxValue;

            foreach (var model in AllPointsList)
            {
                var dist = Vector3.Distance(model.CachedPosition, position);
                if (dist < minimumDistance)
                {
                    nearestPoint = model;
                    minimumDistance = dist;
                }
            }

            return nearestPoint;
        }
    }
}