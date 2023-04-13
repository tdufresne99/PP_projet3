using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionSicknessOption : MonoBehaviour
{
    [SerializeField] private GameObject _vignette;
    void Start()
    {
        _vignette.SetActive(GameManager.instance.motionSicknessOn);
    }
}