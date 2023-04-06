using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
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

    [SerializeField] private RoomLight[] _roomLightsCS;
    [SerializeField] private Transform[] _roomCenterTransforms;
    [SerializeField] private Transform _mapCenterTransform;



    private static RoomManager _instance;
    void Awake()
    {
        if(_instance == null) _instance = this;
        else Destroy(this);
    }

    void Start()
    {
        if(_playerCurrentRoomCS != null) _playerCurrentRoomCS.OnRoomChanged += OnPlayerChangedRoom;
        if(_motherCurrentRoomCS != null) _motherCurrentRoomCS.OnRoomChanged += OnMotherChangedRoom;
        if(_fatherCurrentRoomCS != null) _fatherCurrentRoomCS.OnRoomChanged += OnFatherChangedRoom;
        if(_boyCurrentRoomCS != null) _boyCurrentRoomCS.OnRoomChanged += OnBoyChangedRoom;
        if(_girlCurrentRoomCS != null) _girlCurrentRoomCS.OnRoomChanged += OnGirlChangedRoom;
    }

    private void OnPlayerChangedRoom(RoomsEnum room)
    {
        // Debug.Log("Player is now in " + room);
        _playerCurrentRoom = room;
    }
    private void OnMotherChangedRoom(RoomsEnum room)
    {
        // Debug.Log("Mother is now in " + room);
        _motherCurrentRoom = room;
    }
    private void OnFatherChangedRoom(RoomsEnum room)
    {
        // Debug.Log("Father is now in " + room);
        _fatherCurrentRoom = room;
    }
    private void OnBoyChangedRoom(RoomsEnum room)
    {
        // Debug.Log("Boy is now in " + room);
        _boyCurrentRoom = room;
    }
    private void OnGirlChangedRoom(RoomsEnum room)
    {
        // Debug.Log("Girl is now in " + room);
        _girlCurrentRoom = room;
    }

    public Transform FindRandomRoomWithLightsOn()
    {
        List<Transform> possibleRooms = new List<Transform>(_roomCenterTransforms.Length);
        for (int i = 0; i < _roomCenterTransforms.Length; i++)
        {
            if(_roomLightsCS[i].LightIsOn) possibleRooms.Add(_roomCenterTransforms[i]);
        }
        if(possibleRooms.Count == 0) return _mapCenterTransform;
        else
        {
            int randIndex = Random.Range(0, possibleRooms.Count);
            return possibleRooms[randIndex];
        }
    }

    public RoomLight GetLightToTurnOff(Transform roomTransform)
    {
        var lightToTurnOff = roomTransform.gameObject.GetComponentInParent<RoomLight>();
        if(lightToTurnOff == null) 
        {
            Debug.Log("All lights are turn off...");
        }
        return lightToTurnOff;
    }

    public static RoomManager Instance => _instance;
    public RoomLight[] roomLights => _roomLightsCS;
    public Transform[] roomTransforms => _roomCenterTransforms;
}
