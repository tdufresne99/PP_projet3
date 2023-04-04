using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCurrentRoom : MonoBehaviour
{
    [SerializeField] private RoomsEnum _currentRoom;
    public RoomsEnum CurrentRoom
    {
        get => _currentRoom;
        set
        {
            if(_currentRoom == value) return;
            _currentRoom = value;
            OnRoomChanged?.Invoke(_currentRoom);
        }
    }

    public event Action<RoomsEnum> OnRoomChanged;
}
