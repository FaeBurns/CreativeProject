using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Component responsible for handling a carryable object.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Carryable : MonoBehaviour
{
    /// <summary>
    /// The optional resource this <see cref="Carryable"/> holds.
    /// </summary>
    public Resource Resource;

    [SerializeField] private float dropVelocity = 3f;
    [SerializeField] private Vector3 carryOffset = Vector3.zero;

    private Collider[] colliders;
    private Rigidbody rb;

    /// <summary>
    /// Picks up this <see cref="Carryable"/> and attaches it to the specified transform.
    /// </summary>
    /// <param name="attachTransform">The transform to attach to.</param>
    public void Pickup(Transform attachTransform)
    {
        // disable physics
        colliders.Execute(c => c.enabled = false);
        rb.isKinematic = true;

        // set parent
        transform.SetParent(attachTransform, false);

        // clear position and rotation
        transform.localPosition = carryOffset;
        transform.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// Drops this <see cref="Carryable"/> and places it back on the ground.
    /// </summary>
    public void Drop()
    {
        // enable physics
        colliders.Execute(c => c.enabled = true);
        rb.isKinematic = false;

        rb.velocity = transform.parent.forward * dropVelocity;

        // clear parent
        transform.SetParent(null, true);
    }

    private void Awake()
    {
        colliders = GetComponentsInChildren<Collider>();
        rb = GetComponent<Rigidbody>();
    }
}