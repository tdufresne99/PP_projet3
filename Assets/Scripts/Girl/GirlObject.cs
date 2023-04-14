using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Girl
{
    public class GirlObject : MonoBehaviour
    {
        [SerializeField] private GirlWaypointManager _girlWaypointManager;
        private bool voiceLinePlayed = false;
        public void OnGirlObjectGrabbed()
        {
            Debug.Log(_girlWaypointManager);
            if(voiceLinePlayed == false && _girlWaypointManager != null) 
            {
                _girlWaypointManager.OnWayPointReached(true);
            }
            
            voiceLinePlayed = true;
        }
    }
}
