using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Girl
{
    public class GirlWaypointManager : MonoBehaviour
    {
        [SerializeField] private GirlVoiceLineManager _girlVoicelineManagerCS;
        [SerializeField] private GirlStateManager _girlStateManagerCS;
        [SerializeField] private Transform _girlSpawnPoint;
        [SerializeField] private BoxCollider[] _wayPointsColliders;
        [SerializeField] private Transform[] _wayPoints;
        private Coroutine _CoroutineWayPointReachedCheck;
        private int _currentVoiceLineIndex = -1;
        private int _currentWaypointIndex = -1;
        public Transform _currentWaypoint;
        void Start()
        {
            _girlStateManagerCS.girlTransform.position = _girlSpawnPoint.position;
        }

        public void OnWayPointReached(bool activateWaypoint)
        {
            PlayVoiceLine();
            if(activateWaypoint) ActivateNextWayPoint();
        }

        private void PlayVoiceLine()
        {
            _currentVoiceLineIndex++;
            _girlVoicelineManagerCS.PlayGirlVoicelineIntro(_currentVoiceLineIndex);
        }

        public void ActivateNextWayPoint()
        {
            _currentWaypointIndex++;
            if(_currentWaypointIndex >= _wayPoints.Length) 
            {
                Destroy(this.gameObject);
                return;
            }
            _currentWaypoint = _wayPoints[_currentWaypointIndex];
            
            Invoke("ChangeGirlNavMeshAgentDestination", 1f);
        }

        private void ChangeGirlNavMeshAgentDestination()
        {
            _girlStateManagerCS.TransitionToState(_girlStateManagerCS.walkingState); // to do mech 0 speed
            _girlStateManagerCS.girlNavMeshAgentManager.ChangeDestination(_currentWaypoint.position);
            _CoroutineWayPointReachedCheck = StartCoroutine(CoroutineWayPointReachedCheck());
        }

        private IEnumerator CoroutineWayPointReachedCheck()
        {
            Debug.Log("coroutine check activated");
            while (true)
            {
                if(Vector3.Distance(_girlStateManagerCS.girlTransform.position, _currentWaypoint.position) < 1f)
                {
                    OnWayPointReachedCheck();
                }
                yield return new WaitForSeconds(0.2f);
            }
        }

        private void OnWayPointReachedCheck()
        {
            StopCoroutine(_CoroutineWayPointReachedCheck);

            _girlStateManagerCS.TransitionToState(_girlStateManagerCS.idlingState);
            _wayPointsColliders[_currentWaypointIndex].enabled = true;
        }
    }
}
