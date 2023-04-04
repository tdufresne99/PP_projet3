using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetection : MonoBehaviour
{
    [SerializeField] private int _roomNumber;
    [SerializeField] private LayerMask _monsterLayer;
    private BoxCollider _boxCollider;

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (_monsterLayer == (_monsterLayer | (1 << other.gameObject.layer)))
        {
            Debug.Log("Monster entered Room " + _roomNumber);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (_monsterLayer == (_monsterLayer | (1 << other.gameObject.layer)))
        {
            Debug.Log("Monster exited Room " + _roomNumber);
        }
    }
}
