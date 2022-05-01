using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Component responsible for setting the center of mass on a parent rigidbody.
/// </summary>
public class SetCenterOfMass : MonoBehaviour
{
    [SerializeField] private Rigidbody parentRigidbody;

    private void Awake()
    {
        parentRigidbody.centerOfMass = transform.localPosition;
    }
}
