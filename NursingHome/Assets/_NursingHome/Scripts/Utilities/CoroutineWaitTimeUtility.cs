using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class CoroutineWaitTimeUtility
    {
        public static readonly WaitForEndOfFrame WAIT_FOR_END_OF_FRAME = new WaitForEndOfFrame();
        public static readonly WaitForFixedUpdate WAIT_FOR_FIXED_UPDATE = new WaitForFixedUpdate();

        private static readonly Dictionary<float, WaitForSeconds> _waitForSecondsDict = new Dictionary<float, WaitForSeconds>();
        private static readonly Dictionary<float, WaitForSecondsRealtime> _waitForSecondsRealTimeDict = new Dictionary<float, WaitForSecondsRealtime>();

        public static void AddWaitForSeconds(float time)
        {
            if (!_waitForSecondsDict.ContainsKey(time))
            {
                _waitForSecondsDict.Add(time, new WaitForSeconds(time));
            }
        }

        public static void AddWaitForSecondsRealTime(float time)
        {
            if (!_waitForSecondsRealTimeDict.ContainsKey(time))
            {
                _waitForSecondsRealTimeDict.Add(time, new WaitForSecondsRealtime(time));
            }
        }

        public static WaitForSeconds GetWaitForSeconds(float time)
        {
            WaitForSeconds waitForSeconds;

            if (_waitForSecondsDict.TryGetValue(time, out waitForSeconds))
            {
                return waitForSeconds;
            }

            waitForSeconds = new WaitForSeconds(time);
            _waitForSecondsDict.Add(time, waitForSeconds);

            return waitForSeconds;
        }

        public static WaitForSecondsRealtime GetWaitForSecondsRealTime(float time)
        {
            WaitForSecondsRealtime waitForSecondsRealTime;

            if (_waitForSecondsRealTimeDict.TryGetValue(time, out waitForSecondsRealTime))
            {
                return waitForSecondsRealTime;
            }

            waitForSecondsRealTime = new WaitForSecondsRealtime(time);
            _waitForSecondsRealTimeDict.Add(time, waitForSecondsRealTime);

            return waitForSecondsRealTime;
        }
    }
}