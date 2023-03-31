using NursingHome;
using NursingHome.Interactions;
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

    float eyesightRangeSquared;
    IPlayer player;
    Vector3 lastSeenPosition;

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

        var directionToPlayer = player.GetPlayerAimPosition() - eyesTransform.position;
        var angleToPlayer = Vector3.Angle(eyesTransform.forward, directionToPlayer);

        if(angleToPlayer > inFrontAngle)
        {
            Debug.Log("Not in front");
            CanSeePlayer = false;
            return;
        }

        var distanceToPlayerSqaured = Vector3.SqrMagnitude(directionToPlayer);
        if(distanceToPlayerSqaured > eyesightRangeSquared)
        {
            Debug.Log("Too far");
            CanSeePlayer = false;
            return;
        }

        
        Ray ray = new Ray(eyesTransform.position, directionToPlayer);
        Debug.DrawLine(eyesTransform.position, player.GetPlayerAimPosition(), Color.blue, 0.5f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, eyesightRange, eyesLayerMask))
        {
            if(hit.transform.CompareTag(Tags.Player))
            {
                CanSeePlayer = true;
                lastSeenPosition = player.GetPlayerPosition();
                Debug.Log("Visible");
            }
            else
            {
                Debug.Log("Not visible");
            }
        }
        else
        {
            CanSeePlayer = false;
            Debug.Log("Nothing hit");
        }
    }
}
