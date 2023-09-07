using System;
using System.Collections.Generic;
using OLS_HyperCasual;
using UnityEngine;

namespace OLS_HyperCasual
{
    [ExecuteInEditMode]
    public class MovePointView : MonoBehaviour
    {
        [SerializeField] private List<MovePointView> nearestPoints = new List<MovePointView>();
        [SerializeField] private float radius = 5;
        
        public IReadOnlyList<MovePointView> NearestPoints => nearestPoints;

#if UNITY_EDITOR
        private static List<MovePointView> allMovePointViews = new List<MovePointView>();
        private bool contains;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            var upOffset = new Vector3(0, 0.1f, 0);
            for (int i = nearestPoints.Count - 1; i >= 0; i--)
            {
                var point = nearestPoints[i];
                Gizmos.DrawLine(transform.position + upOffset, point.transform.position + upOffset);
            }
            
            Gizmos.DrawCube(transform.position, new Vector3(0.3f, 0.3f, 0.3f));
        }

        private void Connect(MovePointView view)
        {
            if (nearestPoints.Contains(view))
            {
                return;
            }

            nearestPoints.Add(view);
        }

        private void Disconnect(MovePointView view)
        {
            if (nearestPoints.Contains(view) == false)
            {
                return;
            }

            nearestPoints.Remove(view);
        }

        private void Update()
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (allMovePointViews.Contains(this) == false)
            {
                allMovePointViews.Add(this);
            }

            var currentPos = transform.position;
            var newList = new List<MovePointView>();
            for (int i = allMovePointViews.Count - 1; i >= 0; i--)
            {
                var point = allMovePointViews[i];
                if (point != this)
                {
                    if (Vector3.Distance(point.transform.position, currentPos) <= radius)
                    {
                        newList.Add(point);
                    }
                }
            }

            bool hasChanges = false;
            foreach (var view in newList)
            {
                if (nearestPoints.Contains(view) == false)
                {
                    hasChanges = true;
                    view.Connect(this);
                }
            }

            for (int i = nearestPoints.Count - 1; i >= 0; i--)
            {
                var point = nearestPoints[i];
                if (newList.Contains(point) == false)
                {
                    hasChanges = true;
                    point.Disconnect(this);
                }
            }

            if (hasChanges)
            {
                nearestPoints = newList;
            }
        }

        private void OnDestroy()
        {
            if (allMovePointViews.Contains(this))
            {
                allMovePointViews.Remove(this);
            }
        }
#endif
    }
}