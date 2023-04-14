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
        bool _grabbedDollTooSoon = false;
        void Start()
        {
            _girlStateManagerCS.girlTransform.position = _girlSpawnPoint.position;
        }

        public void OnWayPointReached(bool activateWaypoint, bool objectGrabbed)
        {
            if (objectGrabbed && _currentVoiceLineIndex < 1)
            {
                for (int i = 0; i < _wayPointsColliders.Length; i++)
                {
                    if (_wayPointsColliders[i] != null) _wayPointsColliders[i].enabled = false;
                }
                _grabbedDollTooSoon = true;
                TeleportGirl();
                _currentVoiceLineIndex = 2;
            }
            PlayVoiceLine();
            if (activateWaypoint && !_grabbedDollTooSoon) ActivateNextWayPoint();
        }

        private void PlayVoiceLine()
        {
            _currentVoiceLineIndex++;
            if (_currentVoiceLineIndex == 0) _girlStateManagerCS.girlAudioSource.PlayOneShot(_girlStateManagerCS.girlAlloClip);
                _girlVoicelineManagerCS.PlayGirlVoicelineIntro(_currentVoiceLineIndex);
        }

        public void ActivateNextWayPoint()
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _wayPoints.Length)
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
                Debug.Log(Vector3.Distance(_girlStateManagerCS.girlTransform.position, _currentWaypoint.position));
                if (Vector3.Distance(_girlStateManagerCS.girlTransform.position, _currentWaypoint.position) < 0.15f)
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
            if (_wayPointsColliders[_currentWaypointIndex] != null) _wayPointsColliders[_currentWaypointIndex].enabled = true;
        }

        public void TeleportGirl()
        {
            _girlStateManagerCS.girlTransform.position = _wayPoints[_wayPoints.Length - 1].position;
            _girlStateManagerCS.girlNavMeshAgentManager.ChangeDestination(_wayPoints[_wayPoints.Length - 1].position);
            _girlStateManagerCS.TransitionToState(_girlStateManagerCS.idlingState);
            LookAt lookat = _girlStateManagerCS.gameObject.GetComponent<LookAt>();
            if (lookat != null)
            {
                lookat.enabled = true;
            }
        }
    }
}
