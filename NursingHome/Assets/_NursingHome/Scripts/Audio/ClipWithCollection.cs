using UnityEngine;

namespace Audio
{
    public class ClipWithCollection
    {
        public AudioClip Clip = null;
        public AudioCollection Collection = null;
        public Vector3 Position = Vector3.zero;
        public ulong LoopID = 0;
        public Transform Target = null;
    }
}