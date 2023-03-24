using UnityEngine;

namespace Utilities
{
    public class DrawDebugGizmos : MonoBehaviour
    {
        enum GizmoType
        {
            WireSphere,
            FullSphere
        }

        [SerializeField] GizmoType type;
        [SerializeField] bool alwaysDraw;
        [SerializeField] float radius = 1;
        [SerializeField] Color color = Color.blue;


        void OnDrawGizmos()
        {
            if (!alwaysDraw)
                return;

            DrawGizmos();
        }

        void OnDrawGizmosSelected()
        {
            if (alwaysDraw)
                return;

            DrawGizmos();
        }

        private void DrawGizmos()
        {
            Gizmos.color = color;
            switch (type)
            {
                case GizmoType.WireSphere:
                    Gizmos.DrawWireSphere(transform.position, radius);
                    break;
                case GizmoType.FullSphere:
                    Gizmos.DrawSphere(transform.position, radius);
                    break;

                default:
                    break;
            }
        }
    }
}