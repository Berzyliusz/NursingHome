using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Utilities;

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

        protected ulong ConfigurePoolObject(int poolIndex, AudioClip clip, AudioCollection collection, Vector3 position,
            float startTime, bool ignoreListenerPause, float unimportance)
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
            source.volume = collection.Volume;
            source.spatialBlend = collection._SpatialBlend;
            source.ignoreListenerPause = ignoreListenerPause;
            source.pitch = collection.Pitch;
            source.maxDistance = maxDistance;
            source.rolloffMode = rolloffMode;

            if (collection.RandomPitch)
            {
                float randomValue = Random.Range(-collection.PitchRandomValue, collection.PitchRandomValue);
                source.pitch += randomValue;
            }

            source.outputAudioMixerGroup = _tracks[collection.AudioGroup].Group;

            source.transform.position = position;
            poolItem.Playing = true;
            poolItem.Unimportance = unimportance;
            poolItem.ID = _idGiver;
            source.time = Mathf.Min(startTime, source.clip.length);
            poolItem.GameObject.SetActive(true);
            poolItem.AudioSource.loop = collection.Looping;
            source.Play();

            if(!collection.Looping)
            {
                poolItem.StopRoutine = StopSoundDelayed(_idGiver, source.clip.length);
                StartCoroutine(poolItem.StopRoutine);
            }
            _activePool[_idGiver] = poolItem;
            return _idGiver;
        }

        /// <summary>
        /// Stops sound after a given delay. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        protected IEnumerator StopSoundDelayed(ulong id, float duration)
        {
            yield return CoroutineWaitTimeUtility.GetWaitForSeconds(duration);
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

        /// <summary>
        /// Stops an audio from playing by given ID, if its still playing.
        /// ID is returned when we play audio shot
        /// </summary>
        /// <param name="id">Id of a given sound</param>
        public void StopSound(ulong id)
        {
            AudioPoolItem activeSound;
            if (_activePool.TryGetValue(id, out activeSound))
            {
                CancelStoppingRoutine(activeSound);

                activeSound.AudioSource.Stop();
                activeSound.AudioSource.clip = null;
                activeSound.GameObject.SetActive(false);
                _activePool.Remove(id);

                activeSound.Playing = false;
            }
        }

        void CancelStoppingRoutine(AudioPoolItem activeSound)
        {
            if (activeSound.StopRoutine != null)
            {
                StopCoroutine(activeSound.StopRoutine);
                activeSound.StopRoutine = null;
            }
        }

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

        public ulong PlaySound(AudioClip clip, AudioCollection collection, Vector3 position, float startTime = 0.0f, bool ignoreListenerPause = false)
        {
            if (!_tracks.ContainsKey(collection.AudioGroup))
            {
                return 0;
            }

            if (clip == null || collection.Volume.Equals(0.0f))
            {
                return 0;
            }

            float unimportance = (Camera.main.transform.position - position).sqrMagnitude / Mathf.Max(1, collection.Priority);

            int leastImportantIndex = -1;
            float leastImportanceValue = float.MinValue;

            for (int i = 0; i < _pool.Count; i++)
            {
                AudioPoolItem poolItem = _pool[i];

                if (!poolItem.Playing)
                {
                    return ConfigurePoolObject(i, clip, collection, position, startTime, ignoreListenerPause, unimportance);
                }
                else if (poolItem.Unimportance > leastImportanceValue && !poolItem.AudioSource.loop)
                {
                    leastImportanceValue = poolItem.Unimportance;
                    leastImportantIndex = i;
                }
            }

            if (leastImportanceValue > unimportance)
            {
                return ConfigurePoolObject(leastImportantIndex, clip, collection, position, startTime, ignoreListenerPause, unimportance);
            }

            return 0;
        }
    }
}