using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Girl
{
    public class GirlWaypointManager : MonoBehaviour
    {
        [SerializeField] private GirlStateManager _girlStateManager;
        [SerializeField] private BoxCollider[] _wayPointsColliders;
        [SerializeField] private Transform[] _wayPoints;
        private int _currentWayPointIndex = -1;
        public int currentWayPointIndex
        {
            get => _currentWayPointIndex;
            set 
            {
                if(value >= _wayPoints.Length)
                {
                    Destroy(this);
                }
                _currentWayPointIndex = value;
            }
        }
        public Transform _currentWaypoint;
        void Start()
        {
            if (_wayPoints.Length > 0)
            {
                ActivateNextWayPoint();
            }
            else Debug.LogWarning("Link waypoints in inspector");
        }

        private void DisableCurrentWayPoint()
        {
            _wayPointsColliders[_currentWayPointIndex].enabled = false;
        }

        public void ActivateNextWayPoint()
        {
            DisableCurrentWayPoint();
            currentWayPointIndex++;
            _currentWaypoint = _wayPoints[_currentWayPointIndex];
            _wayPointsColliders[_currentWayPointIndex].enabled = true;
        }
    }
}
