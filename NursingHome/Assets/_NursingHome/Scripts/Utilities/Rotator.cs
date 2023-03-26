using UnityEngine;

namespace Utilities
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField]
        Transform objectToRotate;

        [SerializeField]
        Vector3 rotation;

        void Awake()
        {
            if(!objectToRotate)
            {
                Debug.LogError("No object to rotate @:" + this.gameObject);
                Destroy(gameObject);
            }
        }

        void Update()
        {
            objectToRotate.Rotate(rotation * Time.deltaTime);
        }
    }
}