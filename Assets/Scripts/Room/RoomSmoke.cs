using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSmoke : MonoBehaviour
{
    [SerializeField] private bool _smokeIsOn;
    [SerializeField] private GameObject _smoke;
    void Start()
    {
        if (_smoke != null) _smoke.SetActive(_smokeIsOn);
    }

    public bool SmokeIsOn
    {
        get => _smokeIsOn;
        set
        {
            if(_smokeIsOn == value) return;
            _smokeIsOn = value;
            _smoke.SetActive(_smokeIsOn);
        }
    }
}
