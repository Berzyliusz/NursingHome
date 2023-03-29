using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] AudioMixer mixer = null;
        [SerializeField] int maxSounds = 10;
        [SerializeField] int maxDistance = 50;
        [SerializeField] AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;

        Dictionary<string, TrackInfo> _tracks = new Dictionary<string, TrackInfo>();
        List<AudioPoolItem> _pool = new List<AudioPoolItem>();
        Dictionary<ulong, AudioPoolItem> _activePool = new Dictionary<ulong, AudioPoolItem>();
        ulong _idGiver = 1;

        void Awake()
        {
            if (!mixer)
            {
                return;
            }

            AudioMixerGroup[] groups = mixer.FindMatchingGroups(string.Empty);

            foreach (var group in groups)
            {
                TrackInfo trackInfo = new TrackInfo();
                trackInfo.Name = group.name;
                trackInfo.Group = group;
                trackInfo.TrackFader = null;
                _tracks[group.name] = trackInfo;
            }

            CreatePoolItems();
        }

        private void CreatePoolItems()
        {
            for (int i = 0; i < maxSounds; i++)
            {
                GameObject go = new GameObject("Audio Pool Item");
                AudioSource audioSource = go.AddComponent<AudioSource>();
                go.transform.parent = transform;
                AudioPoolItem poolItem = new AudioPoolItem();
                poolItem.GameObject = go;
                poolItem.AudioSource = audioSource;
                poolItem.Transform = go.transform;
                poolItem.Playing = false;
                go.SetActive(false);
                _pool.Add(poolItem);
            }
        }

        #region Getters
        /// <summary>
        /// Gets a track volume of given track, or float min value, if track is not found
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        public float GetTrackVolume(string track)
        {
            TrackInfo trackInfo;
            if (_tracks.TryGetValue(track, out trackInfo))
            {
                float volume;
                mixer.GetFloat(track, out volume);
                return volume;
            }

            return float.MinValue;
        }

        /// <summary>
        /// Returns audio mixer group by given track, or null if not found
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AudioMixerGroup GetAudioGroupFromTrackName(string name)
        {
            TrackInfo trackInfo;
            if (_tracks.TryGetValue(name, out trackInfo))
            {
                return trackInfo.Group;
            }
            return null;
        }

        /// <summary>
        /// Returns sounds volume, or -1  if sound is not being played
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public float GetSoundVolume(ulong id)
        {
            AudioPoolItem activeSound;
            if (_activePool.TryGetValue(id, out activeSound))
            {
                return activeSound.AudioSource.volume;
            }

            return -1.0f;
        }

        /// <summary>
        /// Returns that sounds pitch, or -1 if sound is not being played
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public float GetSoundPitch(ulong id)
        {
            AudioPoolItem activeSound;
            if (_activePool.TryGetValue(id, out activeSound))
            {
                return activeSound.AudioSource.pitch;
            }

            return -1.0f;
        }
        #endregion

        #region Setters
        /// <summary>
        /// Sets volume of sound. If time is passed, it will act as fader, between current value, and desired value
        /// </summary>
        /// <param name="id"></param>
        /// <param name="volume"></param>
        /// <param name="fadeTime"></param>
        public void SetSoundVolume(ulong id, float volume, float fadeTime = 0.0f)
        {
            AudioPoolItem activeSound;
            if (_activePool.TryGetValue(id, out activeSound))
            {
                if (activeSound.VolumeRoutine != null)
                {
                    StopCoroutine(activeSound.VolumeRoutine);
                }

                if (fadeTime == 0.0f)
                {
                    activeSound.AudioSource.volume = volume;
                }
                else
                {
                    activeSound.VolumeRoutine = SetSoundVolumeRoutine(id, activeSound, volume, fadeTime);
                    StartCoroutine(activeSound.VolumeRoutine);
                }
            }
        }

        /// <summary>
        /// Sets a track param, but it must be exposed in audio mixer. Can do it as a fading in given time, sensitive to pasuing the game or not
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="targetValue"></param>
        /// <param name="fadeTime"></param>
        /// <param name="timeScaleSensitive"></param>
        //public void SetTrackParam(string paramName, float targetValue, float fadeTime = 0.0f, bool timeScaleSensitive = false)
        //{
        //    if (!_mixer)
        //    {
        //        return;
        //    }
        //    TrackInfo trackInfo;
        //    if (_tracks.TryGetValue(paramName, out trackInfo))
        //    {
        //        if (trackInfo.TrackFader != null)
        //        {
        //            StopCoroutine(trackInfo.TrackFader);
        //        }

        //        if (fadeTime == 0.0f)
        //        {
        //            _mixer.SetFloat(paramName, targetValue);
        //        }
        //        else
        //        {
        //            trackInfo.TrackFader = SetTrackVolumeRoutine(paramName, targetValue, fadeTime, timeScaleSensitive);
        //            StartCoroutine(trackInfo.TrackFader);
        //        }
        //    }
        //}
        public void SetTrackParam(string paramName, float targetValue)
        {
            if (!mixer)
            {
                return;
            }
            mixer.SetFloat(paramName, targetValue);
        }

        protected IEnumerator SetTrackVolumeRoutine(string track, float volume, float fadeTime, bool timeScaleSensitive)
        {
            float startVolume = 0.0f;
            float timer = 0.0f;
            mixer.GetFloat(track, out startVolume);

            while (timer < fadeTime)
            {
                if (timeScaleSensitive)
                    timer += Time.deltaTime;
                else
                    timer += Time.unscaledDeltaTime;

                mixer.SetFloat(track, Mathf.Lerp(startVolume, volume, timer / fadeTime));
                yield return null;
            }

            mixer.SetFloat(track, volume);
        }
        protected IEnumerator SetSoundVolumeRoutine(ulong id, AudioPoolItem activeSound, float volume, float fadeTime)
        {
            float startVolume = activeSound.AudioSource.volume;
            float timer = 0.0f;

            while (timer < fadeTime)
            {
                if (!IsAudioPlaying(id)) yield break;
                timer += Time.unscaledDeltaTime;
                activeSound.AudioSource.volume = Mathf.Lerp(startVolume, volume, timer / fadeTime);
                yield return null;
            }

            activeSound.AudioSource.volume = volume;
        }

        #endregion

        #region Internal Audio Playing and pool item configurations
        protected ulong ConfigurePoolObject(int poolIndex, string track, AudioClip clip, Vector3 position, float volume, float spatiaBlend,
            float unimportance, float startTime, bool ignoreListenerPause, float pitch, bool randomPitch, float pitchRandomValue)
        {
            if (poolIndex < 0 || poolIndex >= _pool.Count)
            {
                return 0;
            }

            AudioPoolItem poolItem = _pool[poolIndex];

            if (poolItem.StopRoutine != null) 
                StopCoroutine(poolItem.StopRoutine);

            _idGiver++;
            AudioSource source = poolItem.AudioSource;
            source.clip = clip;
            source.volume = volume;
            source.spatialBlend = spatiaBlend;
            source.ignoreListenerPause = ignoreListenerPause;
            source.pitch = pitch;
            source.maxDistance = maxDistance;
            source.rolloffMode = rolloffMode;

            if (randomPitch)
            {
                float randomValue = Random.Range(-pitchRandomValue, pitchRandomValue);
                source.pitch += randomValue;
            }

            source.outputAudioMixerGroup = _tracks[track].Group;

            source.transform.position = position;
            poolItem.Playing = true;
            poolItem.Unimportance = unimportance;
            poolItem.ID = _idGiver;
            source.time = Mathf.Min(startTime, source.clip.length);
            poolItem.GameObject.SetActive(true);
            poolItem.AudioSource.loop = false;
            source.Play();
            poolItem.StopRoutine = StopSoundDelayed(_idGiver, source.clip.length); // Disable this sound after its length expires
            StartCoroutine(poolItem.StopRoutine);
            _activePool[_idGiver] = poolItem;
            return _idGiver;
        }
        #endregion

        /// <summary>
        /// Stops sound after a given delay. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        protected IEnumerator StopSoundDelayed(ulong id, float duration)
        {
            yield return new WaitForSeconds(duration);
            AudioPoolItem activeSound;
            if (_activePool.TryGetValue(id, out activeSound))
            {
                activeSound.StopRoutine = null;
                activeSound.AudioSource.Stop();
                activeSound.AudioSource.clip = null;
                activeSound.GameObject.SetActive(false);
                _activePool.Remove(id);
                activeSound.Playing = false;
            }
        }

        #region Playing / Paused getters
        /// <summary>
        /// Returns true, if audio if given ID is actually playing, and is not paused.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsAudioPlaying(ulong id)
        {
            if (_activePool.ContainsKey(id))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns true if this audio was playing, but is paused.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsAudioPaused(ulong id)
        {
            if (_activePool.ContainsKey(id))
            {
                AudioPoolItem activeSound;
                if (_activePool.TryGetValue(id, out activeSound))
                {
                    if (activeSound.Playing)
                        return true;
                }

                return true;
            }

            return false;
        }
        #endregion

        #region Playing Methods
        /// <summary>
        /// Plays one shout sound If any of audio sources is available. If this sound if important, it will stop least important sound found, and play it. If its not important, 
        /// and there is no free audio source, it will not be played at all.
        /// </summary>
        /// <param name="track">Audo Mixer track, that we want to play it onto</param>
        /// <param name="clip">Audio Clip to be played</param>
        /// <param name="position">Where do we want to play it</param>
        /// <param name="volume"></param>
        /// <param name="spatialBlend">2D or 3D sound?</param>
        /// <param name="priority">Is this sound important?</param>
        /// <param name="startTime"></param>
        /// <param name="ignoreListenerPause">Should we still play if the pause is on?</param>
        /// <param name="pitch">How high do we want to play this sound?</param>
        /// <param name="randomPitch">Do we want to randomize the pitch?</param>
        /// <param name="pitchRandomValue">How much we add or remove randomly from pitch?</param>
        /// <returns></returns>
        public ulong PlayOneShotSound(string track, AudioClip clip, Vector3 position, float volume, float spatialBlend,
            int priority = 128, float startTime = 0.0f, bool ignoreListenerPause = false, float pitch = 1, bool randomPitch = false, float pitchRandomValue = 0)
        {
            if (!_tracks.ContainsKey(track))
            {
                return 0;
            }

            if (clip == null || volume.Equals(0.0f))
            {
                return 0;
            }

            float unimportance = (Camera.main.transform.position - position).sqrMagnitude / Mathf.Max(1, priority);

            int leastImportantIndex = -1;
            float leastImportanceValue = float.MinValue;

            for (int i = 0; i < _pool.Count; i++)
            {
                AudioPoolItem poolItem = _pool[i];

                if (!poolItem.Playing)
                {
                    return ConfigurePoolObject(i, track, clip, position, volume, spatialBlend, unimportance, startTime, ignoreListenerPause, pitch, randomPitch, pitchRandomValue);
                }
                else if (poolItem.Unimportance > leastImportanceValue && !poolItem.AudioSource.loop)
                {
                    leastImportanceValue = poolItem.Unimportance;
                    leastImportantIndex = i;
                }
            }

            if (leastImportanceValue > unimportance)
            {
                return ConfigurePoolObject(leastImportantIndex, track, clip, position, volume, spatialBlend, unimportance, startTime, ignoreListenerPause, pitch, randomPitch, pitchRandomValue);
            }

            return 0;
        }

        /// <summary>
        /// Plays one shout sound If any of audio sources is available. If this sound if important, it will stop least important sound found, and play it. If its not important, 
        /// and there is no free audio source, it will not be played at all.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="bank"></param>
        /// <param name="position"></param>
        /// <param name="ignoreListenerPause"></param>
        /// <returns></returns>
        public ulong PlayOneShotSound(AudioCollection collection, int bank, Vector3 position, bool ignoreListenerPause = false)
        {
            return PlayOneShotSound
                (
                    collection._AudioGroup,
                    collection[bank],
                    position,
                    collection._Volume,
                    collection._SpatialBlend,
                    collection._Priority,
                    0.0f,
                    ignoreListenerPause,
                    collection._Pitch,
                    collection._RandomPitch,
                    collection._PitchRandomValue
                    );
        }

        /// <summary>
        /// Plays one shout sound If any of audio sources is available. If this sound if important, it will stop least important sound found, and play it. If its not important, 
        /// and there is no free audio source, it will not be played at all.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="clip"></param>
        /// <param name="position"></param>
        /// <param name="ignoreListenerPause"></param>
        /// <returns></returns>
        public ulong PlayOneShotSound(AudioCollection collection, AudioClip clip, Vector3 position, bool ignoreListenerPause = false)
        {
            return PlayOneShotSound
                (
                    collection._AudioGroup,
                    clip,
                    position,
                    collection._Volume,
                    collection._SpatialBlend,
                    collection._Priority,
                    0.0f,
                    ignoreListenerPause,
                    collection._Pitch,
                    collection._RandomPitch,
                    collection._PitchRandomValue
                );
        }

        #endregion
    }
}