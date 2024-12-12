using System.Collections.Generic;
using Framework.Audio;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "SO/Data/GameData", order = 0)]
    public class AudioConfig : ScriptableObject
    {
        [ShowInInspector]
        public Dictionary<AudioClipType, AudioClip> audioClips = new Dictionary<AudioClipType, AudioClip>();
    }
}