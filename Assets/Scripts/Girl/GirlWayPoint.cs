
using UnityEngine;

namespace Girl
{
    public class GirlWayPoint : MonoBehaviour
    {
        [SerializeField] private GirlWaypointManager _wayPointManager;
        private BoxCollider _boxCollider;

        void Start()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }
        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == LayersEnum.PlayerLayer)
            {
                _wayPointManager.ActivateNextWayPoint();
            }
        }
    }    
}