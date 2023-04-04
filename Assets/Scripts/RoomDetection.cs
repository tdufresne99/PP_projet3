using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetection : MonoBehaviour
{
    [SerializeField] private RoomsEnum _room;
    private BoxCollider _boxCollider;

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case LayersEnum.PlayerLayer:
                break;
            case LayersEnum.MotherLayer:
                break;
            case LayersEnum.FatherLayer:
                break;
            case LayersEnum.BoyLayer:
                break;
            case LayersEnum.GirlLayer:
                break;
            default:
                return;
        }
        var objCurrentRoom = other.gameObject.GetComponent<ObjectCurrentRoom>();
        if(objCurrentRoom == null)
        {
            Debug.LogWarning("L'objet ne possède pas de script 'ObjectCurrentRoom'");
            return;
        }
        objCurrentRoom.CurrentRoom = _room;
    }
    void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case LayersEnum.PlayerLayer:
                break;
            case LayersEnum.MotherLayer:
                break;
            case LayersEnum.FatherLayer:
                break;
            case LayersEnum.BoyLayer:
                break;
            case LayersEnum.GirlLayer:
                break;
            default:
                return;
        }
        var objCurrentRoom = other.gameObject.GetComponent<ObjectCurrentRoom>();
        if(objCurrentRoom == null)
        {
            Debug.LogWarning("L'objet ne possède pas de script 'ObjectCurrentRoom'");
            return;
        }
        objCurrentRoom.CurrentRoom = RoomsEnum.Couloir;
    }
}
