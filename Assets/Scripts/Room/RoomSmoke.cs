using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSmoke : MonoBehaviour
{
    [SerializeField] private bool _smokeIsOn;
    [SerializeField] private GameObject _smoke;
    void Start()
    {
        _smoke.SetActive(_smokeIsOn);
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

    private void ToggleSmoke(bool activate)
    {
        // Lumiere.SetActive(activate);
        string lightState = (activate) ? "activé" : "desactivé";
        Debug.Log("La fumée de la salle " + transform.parent.name + " est maintenant " + lightState);
    }
}
