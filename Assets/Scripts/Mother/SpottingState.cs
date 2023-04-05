using System.Collections;
using UnityEngine;

namespace Mother
{
    public class SpottingState : MotherState
    {
        private Coroutine spotCoroutine;
        private float _distanceThreshold = 8f;
        private float _startSpotDelay = 1.5f;
        private float _spotDuration = 2f;
        private float _spotMoveSpeed = 1.5f;
        private MotherStateManager manager;

        public SpottingState(MotherStateManager manager)
        {
            this.manager = manager;
        }
        public override void Enter()
        {
            // Enter patrolling state

            // Play patrol anim;
            manager.GetComponent<MeshRenderer>().material = manager.spotMat;

            // Play patrol sound;

            manager.navMeshDestinationCS.ChangeDestination(manager.playerTransform.position);
            manager.navMeshDestinationCS.ChangeAgentSpeed(_spotMoveSpeed);

            manager.StartCoroutine(StartSpotDelay());
        }

        private IEnumerator StartSpotDelay()
        {
            yield return new WaitForSecondsRealtime(_startSpotDelay);
            StartCoroutineSpot();
        }
        private void StartCoroutineSpot()
        {
            spotCoroutine = manager.StartCoroutine(CoroutineSpot());
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
            while(remaingSpotTime > 0)
            {
                DetectPlayer(manager.motherTransform, manager.playerTransform);
                remaingSpotTime -= 0.1f;
                yield return new WaitForSecondsRealtime(0.1f);
            }
            manager.TransitionToState(manager.patrollingState);
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
            bool isHit = Physics.Raycast(object1Pos, direction, out hit, _distanceThreshold, manager.wallsLayerMask);

            // Check if the raycast hit anything
            if (!isHit || hit.collider.gameObject == otherObjectTransform.gameObject)
            {
                // There are no obstacles in the way, so the two objects have line of sight
                if(spotCoroutine != null) manager.StopCoroutine(spotCoroutine);
                manager.TransitionToState(manager.chasingState);

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