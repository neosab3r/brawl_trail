using System.Collections.Generic;
using UnityEngine;

namespace OLS_HyperCasual
{
    public class PathController : BaseController
    {
        public override bool HasUpdate => true;

        private List<PathModel> models = new List<PathModel>();
        private List<MovePointsCollectorModel> movePointsCollectorModels = new List<MovePointsCollectorModel>();
        private List<MovePointModel> movePointModels = new List<MovePointModel>();
        
        public void AddMovePointCollector(MovePointsCollectorView movePointsCollectorView)
        {
            var allPoints = new List<MovePointModel>();

            foreach (var currentPointView in movePointsCollectorView.PointViews)
            {
                var nearestPoints = new List<MovePointModel>();
                foreach (var nearestPointView in currentPointView.NearestPoints)
                {
                    nearestPoints.Add(AddPathPoint(nearestPointView));
                }

                var model = AddPathPoint(currentPointView);
                model.SetNearestPoints(nearestPoints);
                allPoints.Add(model);
            }
            
            var collectorModel = new MovePointsCollectorModel(movePointsCollectorView, allPoints);
            movePointsCollectorModels.Add(collectorModel);
        }
        
        public void AddPathModel(PathModel model, Vector3 targetPosition)
        {
            model.SetTargetPosition(targetPosition);
            
            var collector = GetCollector();
            var currentModel = collector.GetNearestGlobalPoint(model.CachedTransform.position);
            
            model.SetStartPoint(currentModel);
            
            models.Add(model);
        }
        
        public void RemoveModel(PathModel model)
        {
            models.Remove(model);
        }

        private MovePointsCollectorModel GetCollector()
        {
            return movePointsCollectorModels[0];
        }

        private MovePointModel AddPathPoint(MovePointView view)
        {
            foreach (var pointModel in movePointModels)
            {
                if (pointModel.View == view)
                {
                    return pointModel;
                }
            }

            var model = new MovePointModel(view);
            movePointModels.Add(model);
            return model;
        }
        
        private void MoveToPoint(PathModel model, float dt)
        {
            var currentPointPos = model.CurrentMovePoint.CachedPosition;
            var currentPointDist = Vector3.Distance(currentPointPos, model.CachedTransform.position);
            Debug.Log($"[MoveToPoint]: Point: {model.CurrentMovePoint.View.gameObject} Distance: {currentPointDist}");
            if (currentPointDist <= model.MinPointDistance)
            {
                var nearest = model.CurrentMovePoint.GetNearestToPoint(model.TargetPosition, out bool hasAnyBlocked);
                if (nearest != null)
                {
                    model.SetNextPoint(nearest);
                }
                else
                {
                    model.SetMoveState(false);
                    model.OnReachedPathEnd();
                }

                return;
            }
            
            UpdateNpcPosition(model.CachedTransform, currentPointPos, model.RotationSpeed, model.MoveSpeed, dt);
            model.SetMoveState(true);
        }

        private void UpdateNpcPosition(Transform npcTransform, Vector3 targetPosition, float rotationSpeed, float movespeed, float dt)
        {
            var npcPosition = npcTransform.position;
            targetPosition.y = npcPosition.y;
            npcTransform.position = Vector3.MoveTowards(npcPosition, targetPosition, dt * movespeed);

            var moveDirection = (targetPosition - npcPosition).normalized;
            if (moveDirection != Vector3.zero)
            {
                var rotDirection = Quaternion.LookRotation(moveDirection);
                npcTransform.rotation = Quaternion.Slerp(npcTransform.rotation, rotDirection, rotationSpeed * dt);
            }
        }
        
        public override void Update(float dt)
        {
            for (int i = models.Count - 1; i >=0; i--)
            {
                var model = models[i];
                if (model.HasReachedPathEnd)
                {
                    RemoveModel(model);
                    continue;
                }
                
                MoveToPoint(model, dt);
            }
        }
    }
}