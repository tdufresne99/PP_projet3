using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomManager : MonoBehaviour
{
    [SerializeField] private ObjectCurrentRoom _playerCurrentRoomCS;
    [SerializeField] private ObjectCurrentRoom _motherCurrentRoomCS;
    [SerializeField] private ObjectCurrentRoom _fatherCurrentRoomCS;
    [SerializeField] private ObjectCurrentRoom _boyCurrentRoomCS;
    [SerializeField] private ObjectCurrentRoom _girlCurrentRoomCS;

    [SerializeField] private RoomsEnum _playerCurrentRoom;
    [SerializeField] private RoomsEnum _motherCurrentRoom;
    [SerializeField] private RoomsEnum _fatherCurrentRoom;
    [SerializeField] private RoomsEnum _boyCurrentRoom;
    [SerializeField] private RoomsEnum _girlCurrentRoom;

    void Start()
    {
        _playerCurrentRoomCS.OnRoomChanged += OnPlayerChangedRoom;
        _motherCurrentRoomCS.OnRoomChanged += OnMotherChangedRoom;
        _fatherCurrentRoomCS.OnRoomChanged += OnFatherChangedRoom;
        _boyCurrentRoomCS.OnRoomChanged += OnBoyChangedRoom;
        _girlCurrentRoomCS.OnRoomChanged += OnGirlChangedRoom;
    }

    private void OnPlayerChangedRoom(RoomsEnum room)
    {
        Debug.Log("Player is now in " + room);
        _playerCurrentRoom = room;
    }
    private void OnMotherChangedRoom(RoomsEnum room)
    {
        Debug.Log("Mother is now in " + room);
        _motherCurrentRoom = room;
    }
    private void OnFatherChangedRoom(RoomsEnum room)
    {
        Debug.Log("Father is now in " + room);
        _fatherCurrentRoom = room;
    }
    private void OnBoyChangedRoom(RoomsEnum room)
    {
        Debug.Log("Boy is now in " + room);
        _boyCurrentRoom = room;
    }
    private void OnGirlChangedRoom(RoomsEnum room)
    {
        Debug.Log("Girl is now in " + room);
        _girlCurrentRoom = room;
    }
}
