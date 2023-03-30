using NursingHome;
using System;
using UnityEngine;

public class AiEyes : MonoBehaviour
{
    public bool CanSeePlayer { get; private set; }
    public Vector3 LastPlayerSeenPosition { get; private set; }

    [Tooltip("Place where to fire raycasts from.")]
    [SerializeField] Transform eyesTransform;

    [SerializeField] LayerMask eyesLayerMask;

    [Tooltip("Half of the angle that we consider to be in front of us")]
    [SerializeField] float inFrontAngle = 30.0f;
    [Tooltip("How far can we see?")]
    [SerializeField] float eyesightRange = 30.0f;

    [SerializeField]
    [Tooltip("Draws helpful eyesight gizmos for easier checking of sight")]
    bool drawDebugGizmos;

    IPlayer player;

    void Start()
    {
        player = Systems.Instance.Player;
    }

    void Update()
    {
        // If we DONT need to check sight ( we are searching, we have spotted sth )
            // bail

        var directionToPlayer = player.GetPlayerPosition() - eyesTransform.position;
        var angleToPlayer = Vector3.Angle(eyesTransform.forward, directionToPlayer);

        if(angleToPlayer > inFrontAngle)
        {
            CanSeePlayer = false;
            Debug.Log("NO");
            return;
        }

        Debug.Log("YES");
        // if player is within proper angle in front of our eyesTransform 
        // raycast towards him 

        // if player raycast hit, we see player, store hist last known position
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(eyesTransform.position, eyesTransform.forward * eyesightRange);
    }
}
