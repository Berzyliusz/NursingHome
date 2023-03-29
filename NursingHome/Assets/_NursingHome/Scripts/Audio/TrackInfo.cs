﻿using System.Collections;
using UnityEngine.Audio;

namespace Audio
{
    public class TrackInfo
    {
        public string Name = string.Empty;
        public AudioMixerGroup Group = null;
        public IEnumerator TrackFader = null;
    }
}