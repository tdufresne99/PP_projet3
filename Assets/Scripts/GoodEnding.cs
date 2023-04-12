using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodEnding : MonoBehaviour
{
    [SerializeField] private EndingManager _endingManagerCS;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayersEnum.Book)
        {
            other.gameObject.layer = LayersEnum.NoPlayerInteraction;
            _endingManagerCS.OnGoodEnding();
        }
    }
}
