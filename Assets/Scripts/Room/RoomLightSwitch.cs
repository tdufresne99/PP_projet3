using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLightSwitch : MonoBehaviour
{
    [SerializeField] private RoomLight _roomLightCS;
    [SerializeField] private MeshRenderer _switchMeshRenderer;
    [SerializeField] private Material _onMat;
    [SerializeField] private Material _offMat;
    private bool _canInteractWithSwitch = true;
    private float _lightInteractionDelay = 0.5f;
    
    void Start()
    {
        _switchMeshRenderer.material = _roomLightCS.LightIsOn ? _onMat : _offMat;
    }
    public void ToggleSwitch()
    {
        Debug.Log("Toggle switch");
        if (_canInteractWithSwitch == false) return;

        _canInteractWithSwitch = false;
        Invoke("ActivateSwitchInteraction", _lightInteractionDelay);
        _roomLightCS.LightIsOn = !_roomLightCS.LightIsOn;
        _switchMeshRenderer.material = _roomLightCS.LightIsOn ? _onMat : _offMat;
    }

    private void ActivateSwitchInteraction()
    {
        _canInteractWithSwitch = true;
    }
}
