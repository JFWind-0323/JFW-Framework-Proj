using System.Collections.Generic;
using Framework.Audio;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "SO/Data/GameConfig", order = 0)]
    public class AudioConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [ShowInInspector]
        public Dictionary<AudioClipType, AudioClip> audioClips = new Dictionary<AudioClipType, AudioClip>();

        // 用于Odin序列化的字段
        [SerializeField, HideInInspector] private List<AudioClipType> audioClipKeys = new List<AudioClipType>();

        [SerializeField, HideInInspector] private List<AudioClip> audioClipValues = new List<AudioClip>();

        // 序列化前调用
        public void OnBeforeSerialize()
        {
            audioClipKeys.Clear();
            audioClipValues.Clear();
            foreach (var kvp in audioClips)
            {
                audioClipKeys.Add(kvp.Key);
                audioClipValues.Add(kvp.Value);
            }
        }

        // 序列化后调用
        public void OnAfterDeserialize()
        {
            audioClips.Clear();
            for (int i = 0; i < audioClipKeys.Count; i++)
            {
                audioClips[audioClipKeys[i]] = audioClipValues[i];
            }
        }
    }
}