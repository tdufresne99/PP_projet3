using System.Collections;
using UnityEngine;

namespace Mother
{
    public class SpottingState : MotherState
    {
        private Coroutine spotCoroutine;
        private float _distanceThreshold = 12f;
        private float _startSpotDelay = 1.5f;
        private float _spotDuration = 2f;
        private float _spotMoveSpeed = 2.5f;
        private MotherStateManager _manager;
        private string _runTrigger = "run";

        public SpottingState(MotherStateManager manager)
        {
            this._manager = manager;
        }
        public override void Enter()
        {
            // Enter spotting state

            // Play patrol anim;
            _manager.motherAnimator.SetTrigger(_runTrigger);
            _manager.motherAnimator.speed = 0.4f;

            // Play patrol sound;
            _manager.motherAudioSource.PlayOneShot(_manager.motherSpot);

            _manager.navMeshDestinationCS.ChangeDestination(_manager.playerTransform.position);
            _manager.navMeshDestinationCS.ChangeAgentSpeed(_spotMoveSpeed);

            _manager.StartCoroutine(StartSpotDelay());
        }

        private IEnumerator StartSpotDelay()
        {
            yield return new WaitForSecondsRealtime(_startSpotDelay);
            StartCoroutineSpot();
        }
        private void StartCoroutineSpot()
        {
            spotCoroutine = _manager.StartCoroutine(CoroutineSpot());
        }

        public override void Execute()
        {
            // Do patrolling behavior
        }

        public override void Exit()
        {
            // Exit patrolling state
        }

        private IEnumerator CoroutineSpot()
        {
            var remaingSpotTime = _spotDuration;
            while (remaingSpotTime > 0)
            {
                DetectPlayer(_manager.motherTransform, _manager.playerTransform);
                remaingSpotTime -= 0.1f;
                yield return new WaitForSecondsRealtime(0.1f);
            }
            _manager.TransitionToState(_manager.patrollingState);
        }

        private void DetectPlayer(Transform objectTransform, Transform otherObjectTransform)
        {
            // Get the position of the two GameObjects
            Vector3 object1Pos = objectTransform.position;
            Vector3 object2Pos = otherObjectTransform.position;

            // Check if the two objects are within the maximum distance for the line of sight check
            if ((object1Pos - object2Pos).sqrMagnitude > _distanceThreshold * _distanceThreshold)
            {
                // The two objects are too far apart for a line of sight check, do not perform raycast
                return;
            }

            // Find the direction from object1 to object2
            Vector3 direction = object2Pos - object1Pos;

            // Set up the raycast hit information
            RaycastHit hit;
            bool isHit = Physics.Raycast(object1Pos, direction, out hit, _distanceThreshold, _manager.wallsLayerMask);

            // Check if the raycast hit anything
            if (!isHit || hit.collider.gameObject == otherObjectTransform.gameObject)
            {
                // There are no obstacles in the way, so the two objects have line of sight
                if (spotCoroutine != null) _manager.StopCoroutine(spotCoroutine);
                _manager.TransitionToState(_manager.chasingState);

                // Visualize the check by drawing a line between the two objects
                Debug.DrawLine(object1Pos, object2Pos, Color.green, 0.1f);
            }
            else
            {
                // There is an obstacle in the way, so the two objects do not have line of sight
                // Visualize the check by drawing a line between the two objects up to the point of the hit
                Debug.DrawLine(object1Pos, hit.point, Color.red, 0.1f);
            }
        }
    }
}