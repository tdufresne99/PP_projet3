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
            if(voiceLinePlayed == false) _girlWaypointManager.OnWayPointReached(true);
            voiceLinePlayed = true;
        }
    }
}
