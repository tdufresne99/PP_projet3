using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetection : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _walls;
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private Material _redMaterial;
    [SerializeField] private int _roomNumber;
    [SerializeField] private LayerMask _playerLayer;
    private BoxCollider _boxCollider;

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (_playerLayer == (_playerLayer | (1 << other.gameObject.layer)))
        {
            foreach (var wall in _walls)
            {
                wall.material = _greenMaterial;
            }
            Debug.Log("Player entered Room " + _roomNumber);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (_playerLayer == (_playerLayer | (1 << other.gameObject.layer)))
        {
            foreach (var wall in _walls)
            {
                wall.material = _redMaterial;
            }
            Debug.Log("Player exited Room " + _roomNumber);
        }
    }
}
