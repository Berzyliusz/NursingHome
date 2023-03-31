using NursingHome;
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

    float eyesightRangeSquared;

    IPlayer player;

    void Awake()
    {
        eyesightRangeSquared = eyesightRange * eyesightRange;
    }

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
            Debug.Log("Not in front");
            return;
        }

        var distanceToPlayerSqaured = Vector3.SqrMagnitude(directionToPlayer);
        if(distanceToPlayerSqaured > eyesightRangeSquared)
        {
            CanSeePlayer = false;
            Debug.Log("too far");
            return;
        }

        Ray ray = new Ray(eyesTransform.position, player.GetPlayerAimPosition());
        if(Physics.Raycast(ray, eyesightRange, eyesLayerMask))
        {
            CanSeePlayer = true;
            Debug.Log("Visible");
        }
        else
        {
            CanSeePlayer = false;
            Debug.Log("Not visible");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(eyesTransform.position, eyesTransform.forward * eyesightRange);
    }
}
