using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleKiller : MonoBehaviour
{
    [SerializeField] private string destroableTag;
    private void OnCollisionEnter2D(Collision2D _other)
    {
        if (_other.gameObject.CompareTag(destroableTag))
        {
            Destroy(_other.gameObject);
        }
    }
}
