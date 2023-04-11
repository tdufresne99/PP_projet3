using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boy;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private BoyStateManager _boyManagerCS;
    [SerializeField] private LayerMask _boyLayerMask;
    private float raycastLength = 10f;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastLength, _boyLayerMask))
        {
            if (hit.collider != null)
            {
                _boyManagerCS.TransitionToState(_boyManagerCS.respawningState);
                Debug.DrawRay(transform.position, transform.forward * raycastLength, Color.green);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * raycastLength, Color.red);
        }
    }
}
