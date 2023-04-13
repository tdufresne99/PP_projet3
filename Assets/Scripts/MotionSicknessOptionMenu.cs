using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionSicknessOptionMenu : MonoBehaviour
{
    [SerializeField] private GameObject _vignette;
    private bool _isOn = false;
    
    public void ToggleVignette()
    {
        _isOn = !_isOn;
        _vignette.SetActive(_isOn);
    }
}
