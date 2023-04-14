using UnityEngine;

namespace Mother
{
    public class ChasingState : MotherState
    {
        private float _chaseMoveSpeed = 3f;
        private float _distanceThreshold = 12f;
        private MotherStateManager _manager;

        private string _runTrigger = "run";

        public ChasingState(MotherStateManager manager)
        {
            this._manager = manager;
        }

        public override void Enter()
        {
            // Enter patrolling state

            // Play chase anim;
            _manager.motherAnimator.SetTrigger(_runTrigger);
            _manager.motherAnimator.speed = 0.5f;


            // Play chase sound;
            _manager.motherAudioSource.clip = _manager.motherChase;
            _manager.motherAudioSource.loop = true;
            _manager.motherAudioSource.Play();

            _manager.navMeshDestinationCS.ChangeDestination(_manager.playerTransform.position);
            _manager.navMeshDestinationCS.ChangeAgentSpeed(_chaseMoveSpeed);
        }

        public override void Execute()
        {
            // Do patrolling behavior
            _manager.navMeshDestinationCS.ChangeDestination(_manager.playerTransform.position);
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

                // Visualize the check by drawing a line between the two objects
                Debug.DrawLine(object1Pos, object2Pos, Color.green, 0.1f);
            }
            else
            {
                // There is an obstacle in the way, so the two objects do not have line of sight
                // Visualize the check by drawing a line between the two objects up to the point of the hit
                _manager.TransitionToState(_manager.spottingState);

                Debug.DrawLine(object1Pos, hit.point, Color.red, 0.1f);
            }
        }
    }
}
