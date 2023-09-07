using System.Collections.Generic;
using UnityEngine;

namespace OLS_HyperCasual
{
    public class MovePointModel : BaseModel<MovePointView>
    {
        public bool IsBlocked { get; set; }
        public Vector3 CachedPosition { get; }
        private List<MovePointModel> nearestPoints;
        
        public MovePointModel(MovePointView view)
        {
            View = view;
            CachedPosition = View.transform.position;
        }

        public void SetNearestPoints(List<MovePointModel> nearestPoints)
        {
            this.nearestPoints = nearestPoints;
        }
        
        public MovePointModel GetNearestToPoint(Vector3 targetPoint, out bool hasAnyBlockedPoint)
        {
            var selfDistance = Vector3.Distance(CachedPosition, targetPoint);
            hasAnyBlockedPoint = false;
            foreach (var pointView in nearestPoints)
            {
                if (Vector3.Distance(pointView.CachedPosition, targetPoint) < selfDistance)
                {
                    hasAnyBlockedPoint = true;
                    if (pointView.IsBlocked == false)
                    {
                        return pointView;
                    }
                }
            }

            return null;
        }
    }
}