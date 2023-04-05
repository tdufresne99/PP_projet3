
using UnityEngine;

namespace Mother
{
    public class PatrollingState : MotherState
    {
        private float _patrolMoveSpeed = 2f;
        private MotherStateManager _manager;
        private float _distanceThreshold = 8f;
        
        public PatrollingState(MotherStateManager manager)
        {
            this._manager = manager;
        }
        public override void Enter()
        {
            // Enter patrolling state

            // Play patrol anim;
            _manager.GetComponent<MeshRenderer>().material = _manager.patrolMat;

            // Play patrol sound;
            _manager.navMeshDestinationCS.ChangeDestination();
            _manager.navMeshDestinationCS.ChangeAgentSpeed(_patrolMoveSpeed);
        }

        public override void Execute()
        {
            // Do patrolling behavior
            DetectPlayer(_manager.motherTransform, _manager.playerTransform);
        }

        public override void Exit()
        {
            // Exit patrolling state
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
                _manager.TransitionToState(_manager.spottingState);

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

