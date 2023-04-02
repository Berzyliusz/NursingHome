using Sirenix.OdinInspector;
using UnityEngine;

namespace NursingHome
{
    public class PlayerWrapper : MonoBehaviour, IPlayer
    {
        [SerializeField]
        [SceneObjectsOnly]
        Transform aimPoint;

        [SerializeField]
        MonoBehaviour playerScript;

        Transform myTransform;

        void Awake()
        {
            myTransform = transform;
        }

        public Vector3 GetPlayerAimPosition()
        {
            return aimPoint.position;
        }

        public Vector3 GetPlayerPosition()
        {
            return myTransform.position;
        }

        public void SetFreezePlayer(bool isFrozen)
        {
            playerScript.enabled = !isFrozen;
        }
    }
}