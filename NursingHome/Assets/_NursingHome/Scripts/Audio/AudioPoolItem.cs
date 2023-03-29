using System.Collections;
using UnityEngine;

namespace Audio
{
    public class AudioPoolItem
    {
        public GameObject GameObject = null;
        public Transform Transform = null;
        public Transform TargetToFollow = null;
        public AudioSource AudioSource = null;
        public float Unimportance = float.MaxValue;
        public bool Playing = false;
        public IEnumerator StopRoutine = null;
        public IEnumerator VolumeRoutine = null;
        public ulong ID = 0;
    }
}