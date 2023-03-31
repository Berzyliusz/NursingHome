using NursingHome.Interactions;
using UnityEngine;

namespace NursingHome.AI
{
    public interface IEyes : IUpdateable
    {
        bool CanSeePlayer { get; }
        Vector3 LastPlayerSeenPosition { get; }

        void SetEyesightChecking(bool shouldCheckEyesight);
    }

    public class AiEyes : IEyes
    {
        public bool CanSeePlayer { get; private set; }
        public Vector3 LastPlayerSeenPosition { get; private set; }

        readonly Transform eyesTransform;
        readonly float eyesightRangeSquared;
        readonly IPlayer player;
        readonly AIParams aiParams;
        bool shouldCheckEyesight;

        public AiEyes(IPlayer player, AIParams aiParams, Transform eyesTransform)
        {
            this.player = player;
            this.aiParams = aiParams;
            this.eyesTransform = eyesTransform;

            eyesightRangeSquared = aiParams.EyesightRange * aiParams.EyesightRange;
        }

        public void SetEyesightChecking(bool shouldCheckEyesight)
        {
            this.shouldCheckEyesight = shouldCheckEyesight;
        }

        public void Update(float deltaTime)
        {
            if (!shouldCheckEyesight)
                return;

            var directionToPlayer = player.GetPlayerAimPosition() - eyesTransform.position;
            var angleToPlayer = Vector3.Angle(eyesTransform.forward, directionToPlayer);

            if (angleToPlayer > aiParams.InFrontAngle)
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
            Debug.Log($"Can see player: {CanSeePlayer}");
        }

        void RaycastToPlayer(Vector3 directionToPlayer)
        {
            Ray ray = new Ray(eyesTransform.position, directionToPlayer);
            Debug.DrawLine(eyesTransform.position, player.GetPlayerAimPosition(), Color.blue, 0.5f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, aiParams.EyesightRange, aiParams.EyesLayerMask))
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
}