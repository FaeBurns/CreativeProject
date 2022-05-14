using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A component responsible for allowing a particle system to follow an object without issues.
/// </summary>
public class ParticleFollow : MonoBehaviour
{
    [Header("Readonly")]
    [SerializeField] private Transform followTarget;
    [SerializeField] private Rigidbody followTargetRigidBody;

    [Header("Fields")]
    [SerializeField] private float velocityMultiplier = 1f;

    /// <summary>
    /// Gets or Sets the transform to follow.
    /// </summary>
    public Transform FollowTarget
    {
        get => followTarget;
        set
        {
            followTarget = value;
            followTargetRigidBody = followTarget.GetComponent<Rigidbody>();
        }
    }

    private void Awake()
    {
        // force update of cached rigidbody
        FollowTarget = followTarget;
    }

    private void FixedUpdate()
    {
        // if target exists
        if (FollowTarget != null)
        {
            // set position
            // do not set rotation
            transform.position = FollowTarget.position + (followTargetRigidBody.velocity * velocityMultiplier);
        }
    }
}