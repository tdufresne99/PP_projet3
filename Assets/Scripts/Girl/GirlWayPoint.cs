
using UnityEngine;

namespace Girl
{
    public class GirlWayPoint : MonoBehaviour
    {
        [SerializeField] private GirlWaypointManager _wayPointManager;
        [SerializeField] private bool _activateWaypoint;
        private BoxCollider _boxCollider;

        void Start()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayersEnum.PlayerLayer)
            {
                if (_boxCollider != null) Destroy(_boxCollider);
                if (_wayPointManager != null) _wayPointManager.OnWayPointReached(_activateWaypoint, false);
            }
        }
    }
}