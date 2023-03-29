using System.Collections.Generic;

using UnityEngine;
namespace Audio
{
    [System.Serializable]
    public class ClipBank
    {
        [Tooltip("Debug name, just for orientation and easy finding of necessities")]
        public string Name = string.Empty;
        public AudioClip[] Clips = new AudioClip[0];
    }

    [CreateAssetMenu(fileName = "AudioCollection", menuName = "Audio/AudioCollection", order = 0)]
    public class AudioCollection : ScriptableObject
    {
        #region Serialized
        [Header("Main Properties")]
        [SerializeField] string _audioGroup = string.Empty;
        [SerializeField][Range(0.0f, 1.0f)] float _volume = 0.5f;
        [SerializeField][Range(0.0f, 1.0f)] float _spatialBlend = 1.0f;
        [SerializeField][Range(0, 256)] int _priority = 128;
        [Header("Pitch Controls")]
        [Tooltip("Used for changing the collections pitch and random pitch values")]
        [SerializeField][Range(0.0f, 2.0f)] float _pitch = 1.0f;
        [SerializeField][Range(0.0f, 1.0f)] float _pitchRandomValue = 0.0f;
        [SerializeField] bool _randomPitch = false;
        [Header("Clip Banks")]
        [Tooltip("Store AudioClips here under chosen bank number")]
        [SerializeField] ClipBank[] _audioClipBanks = new ClipBank[0];
        #endregion

        #region Getters
        public ClipBank[] _AudioBanks { get { return _audioClipBanks; } }
        public string _AudioGroup { get { return _audioGroup; } }
        public float _Volume { get { return _volume; } }
        public float _SpatialBlend { get { return _spatialBlend; } }
        public int _Priority { get { return _priority; } }
        public int _BankCount { get { return _audioClipBanks.Length; } }
        public float _Pitch { get { return _pitch; } }
        public float _PitchRandomValue { get { return _pitchRandomValue; } }
        public bool _RandomPitch { get { return _randomPitch; } }
        #endregion

        public AudioClip this[int i]
        {
            get
            {
                if (_audioClipBanks == null || _audioClipBanks.Length <= i) return null;
                if (_audioClipBanks[i].Clips.Length == 0) return null;

                int randomNumId = Random.Range(0, _audioClipBanks[i].Clips.Length);
                AudioClip clip = _audioClipBanks[i].Clips[randomNumId];
                return clip;
            }
        }

        public AudioClip AudioClip
        {
            get
            {
                if (_audioClipBanks == null || _audioClipBanks.Length == 0) return null;
                if (_audioClipBanks[0].Clips.Length == 0) return null;

                int randomNumId = Random.Range(0, _audioClipBanks[0].Clips.Length);
                AudioClip clip = _audioClipBanks[0].Clips[randomNumId];
                return clip;
            }
        }
    }
}