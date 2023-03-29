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
        [SerializeField] bool _randomPitch;
        [SerializeField] bool _looping;
        [Header("Clip Banks")]
        [Tooltip("Store AudioClips here under chosen bank number")]
        [SerializeField] ClipBank[] _audioClipBanks = new ClipBank[0];
        #endregion

        #region Getters
        public ClipBank[] _AudioBanks => _audioClipBanks;
        public string _AudioGroup => _audioGroup;
        public float _Volume => _volume;
        public float _SpatialBlend => _spatialBlend;
        public int _Priority => _priority;
        public int _BankCount => _audioClipBanks.Length;
        public float _Pitch => _pitch;
        public float _PitchRandomValue => _pitchRandomValue;
        public bool _RandomPitch => _randomPitch;
        public bool Looping => _looping;
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