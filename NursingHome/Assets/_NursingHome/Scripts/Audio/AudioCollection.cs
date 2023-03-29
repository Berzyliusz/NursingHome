using System.Collections.Generic;

using UnityEngine;
namespace Audio
{
    [System.Serializable]
    public class ClipBank
    {
        [Tooltip("Debug name, just for orientation and easy finding of necessities")]
        public string Name = string.Empty;
        public List<AudioClip> Clips = new List<AudioClip>();
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
        [SerializeField] List<ClipBank> _audioClipBanks = new List<ClipBank>();
        #endregion

        #region Getters
        public List<ClipBank> _AudioBanks { get { return _audioClipBanks; } }
        public string _AudioGroup { get { return _audioGroup; } }
        public float _Volume { get { return _volume; } }
        public float _SpatialBlend { get { return _spatialBlend; } }
        public int _Priority { get { return _priority; } }
        public int _BankCount { get { return _audioClipBanks.Count; } }
        public float _Pitch { get { return _pitch; } }
        public float _PitchRandomValue { get { return _pitchRandomValue; } }
        public bool _RandomPitch { get { return _randomPitch; } }
        #endregion

        public AudioClip this[int i]
        {
            get
            {
                if (_audioClipBanks == null || _audioClipBanks.Count <= i) return null;
                if (_audioClipBanks[i].Clips.Count == 0) return null;

                int randomNumId = Random.Range(0, _audioClipBanks[i].Clips.Count);
                AudioClip clip = _audioClipBanks[i].Clips[randomNumId];
                return clip;
            }
        }

        public AudioClip AudioClip
        {
            get
            {
                if (_audioClipBanks == null || _audioClipBanks.Count == 0) return null;
                if (_audioClipBanks[0].Clips.Count == 0) return null;

                List<AudioClip> clipList = _audioClipBanks[0].Clips;
                int randomNumId = Random.Range(0, clipList.Count);

                AudioClip clip = clipList[randomNumId];
                return clip;
            }
        }
    }
}