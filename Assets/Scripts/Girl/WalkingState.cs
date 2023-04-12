using UnityEngine;

namespace Girl
{
    public class WalkingState : GirlState
    {
        private float _walkingSpeed = 3.5f;
        private GirlStateManager _manager;
        private float _distanceThreshold = 6f;
        private bool _isWalking = true;

        public WalkingState(GirlStateManager manager)
        {
            this._manager = manager;
        }
        public override void Enter()
        {
            Debug.Log("girl is walking");
            // Play walk anim
            _isWalking = true;
        }

        public override void Execute()
        {
            if(DetectPlayer(_manager.girlTransform, _manager.playerTransform))
            {
                if(_isWalking == false)
                {
                    // Play walk anim
                    _isWalking = true;
                }
                _manager.girlNavMeshAgentManager.ChangeAgentSpeed(_walkingSpeed);
            }
            else
            {
                if(_isWalking == true)
                {
                    // Play idle anim
                    _isWalking = false;
                }
                _manager.girlNavMeshAgentManager.ChangeAgentSpeed(0);
            }
        }

        public override void Exit()
        {
            
        }
        private bool DetectPlayer(Transform objectTransform, Transform otherObjectTransform)
        {
            // Get the position of the two GameObjects
            Vector3 object1Pos = objectTransform.position;
            Vector3 object2Pos = otherObjectTransform.position;

            // Check if the two objects are within the maximum distance for the line of sight check
            if ((object1Pos - object2Pos).sqrMagnitude > _distanceThreshold * _distanceThreshold)
            {
                // The two objects are too far apart for a line of sight check, do not perform raycast
                return false;
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
                return true;
            }
            else
            {
                // There is an obstacle in the way, so the two objects do not have line of sight
                // Visualize the check by drawing a line between the two objects up to the point of the hit
                Debug.DrawLine(object1Pos, hit.point, Color.red, 0.1f);
                return false;
            }
        }
    }
}