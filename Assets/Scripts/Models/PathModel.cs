using System;
using UnityEngine;

namespace OLS_HyperCasual
{
    public class PathModel : BaseModel<MonoBehaviour>
    {
        public bool HasReachedPathEnd { get; private set; }
        public Vector3 TargetPosition { get; private set; }
        public MovePointModel StartPoint { get; private set; }
        public MovePointModel CurrentMovePoint { get; private set; }
        public MovePointModel LastMovePoint { get; private set; }
        public Transform CachedTransform { get; }
        public float MoveSpeed { get; }
        public float RotationSpeed { get; }
        public float MinPointDistance { get; }
        public Action OnReachedEndPosition;

        public PathModel(MonoBehaviour view, float moveSpeed, float rotationSpeed, float minPointDistance = 0.35f)
        {
            View = view;
            CachedTransform = view.transform;
            MoveSpeed = moveSpeed;
            RotationSpeed = rotationSpeed;
            MinPointDistance = minPointDistance;
        }

        public void SetTargetPosition(Vector3 position)
        {
            HasReachedPathEnd = false;
            TargetPosition = position;
        }

        public void SetStartPoint(MovePointModel point)
        {
            StartPoint = point;
            CurrentMovePoint = point;
            point.IsBlocked = true;
        }
        
        public void SetNextPoint(MovePointModel nextPoint)
        {
            if (LastMovePoint != null)
            {
                LastMovePoint.IsBlocked = false;
            }
            if (CurrentMovePoint != null)
            {
                CurrentMovePoint.IsBlocked = false;
            }
        
            nextPoint.IsBlocked = true;
        
            LastMovePoint = CurrentMovePoint;
            CurrentMovePoint = nextPoint;
            HasReachedPathEnd = false;
        }

        public void SetMoveState(bool isMoving)
        {
            
        }
        
        public void OnReachedPathEnd()
        {
            HasReachedPathEnd = true;

            if (CurrentMovePoint != null)
            {
                CurrentMovePoint.IsBlocked = false;
            }
            
            OnReachedEndPosition?.Invoke();
        }
    }
}