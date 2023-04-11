using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private PlayerDeathManager _playerDeathManagerCS;

    public void ResetPlayerPosition()
    {
        _playerDeathManagerCS.ResetPlayerPosition();
    }
}
