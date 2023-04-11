using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private CapsuleCollider _hitbox;   

    void Start()
    {
        _hitbox.isTrigger = true;
    }
}
