using NursingHome;
using NursingHome.Interactions;
using UnityEngine;

public class AiEyes : MonoBehaviour
{
    public bool CanSeePlayer { get; private set; }
    public Vector3 LastPlayerSeenPosition { get; private set; }

    [Tooltip("Place where to fire raycasts from.")]
    [SerializeField] Transform eyesTransform;
    [Tooltip("Layer mask for player, and things we consider, that can obstruct him")]
    [SerializeField] LayerMask eyesLayerMask;

    [Tooltip("Half of the angle that we consider to be in front of us")]
    [SerializeField] float inFrontAngle = 30.0f;
    [Tooltip("How far can we see?")]
    [SerializeField] float eyesightRange = 30.0f;

    [SerializeField]
    bool shouldCheckEyesight;

    float eyesightRangeSquared;
    IPlayer player;
    

    public void SetEyesightChecking(bool shouldCheckEyesight)
    {
        this.shouldCheckEyesight = shouldCheckEyesight;
    }

    void Awake()
    {
        eyesightRangeSquared = eyesightRange * eyesightRange;
    }

    void Start()
    {
        //Todo: Make player injected here
        player = Systems.Instance.Player;
    }

    void Update()
    {
        if (!shouldCheckEyesight)
            return;

        var directionToPlayer = player.GetPlayerAimPosition() - eyesTransform.position;
        var angleToPlayer = Vector3.Angle(eyesTransform.forward, directionToPlayer);

        if (angleToPlayer > inFrontAngle)
        {
            CanSeePlayer = false;
            return;
        }

        var distanceToPlayerSqaured = Vector3.SqrMagnitude(directionToPlayer);
        if (distanceToPlayerSqaured > eyesightRangeSquared)
        {
            CanSeePlayer = false;
            return;
        }

        RaycastToPlayer(directionToPlayer);
    }

    void RaycastToPlayer(Vector3 directionToPlayer)
    {
        Ray ray = new Ray(eyesTransform.position, directionToPlayer);
        Debug.DrawLine(eyesTransform.position, player.GetPlayerAimPosition(), Color.blue, 0.5f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, eyesightRange, eyesLayerMask))
        {
            if (hit.transform.CompareTag(Tags.Player))
            {
                CanSeePlayer = true;
                LastPlayerSeenPosition = player.GetPlayerPosition();
            }
            else
            {
                CanSeePlayer = false;
            }
        }
        else
        {
            CanSeePlayer = false;
        }
    }
}
