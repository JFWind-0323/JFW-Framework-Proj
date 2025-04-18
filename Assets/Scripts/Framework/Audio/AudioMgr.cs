using Framework.Singleton;
using UnityEngine;

//音频管理器
namespace Framework.Audio
{
    public class AudioMgr : MonoSingle<AudioMgr>
    {
        private struct Channel
        {
            public AudioSource channel;
            public float keyOnTime; //记录最近一次播放音乐的时刻
        }

        // 整个游戏中，总的音源数量
        private const int AUDIO_CHANNEL_NUM = 8;
        private Channel[] channels;

        protected override void WhenInit()
        {
            channels = new Channel[AUDIO_CHANNEL_NUM];
            for (var i = 0; i < AUDIO_CHANNEL_NUM; i++)
            {
                //每个频道对应一个音源
                channels[i].channel = gameObject.AddComponent<AudioSource>();
                channels[i].keyOnTime = 0;
            }
        }
        

        /// <summary>
        /// 播放一次，参数为音频片段、音量、左右声道、速度,这个方法主要用于音效，因此考虑了音效顶替的逻辑
        /// </summary>
        /// <param dataName="clip"></param>
        /// <param dataName="volume"></param>
        /// <param dataName="pan"></param>
        /// <param dataName="pitch"></param>
        /// <returns></returns>
        public int PlayOneShot(AudioClip clip, float volume, float pan, float pitch = 1.0f)
        {
            for (var i = 0; i < channels.Length; i++)
                //如果正在播放同一个片段，而且刚刚才开始，则直接退出函数
                if (channels[i].channel.isPlaying &&
                    channels[i].channel.clip == clip &&
                    channels[i].keyOnTime >= Time.time - 0.03f)
                    return -1;
            //遍历所有频道，如果有频道空闲直接播放新音频，并退出
            //如果没有空闲频道，先找到最开始播放的频道（oldest），稍后使用
            var oldest = -1;
            var time = 10000000.0f;
            for (var i = 0; i < channels.Length; i++)
            {
                if (channels[i].channel.loop == false &&
                    channels[i].channel.isPlaying &&
                    channels[i].keyOnTime < time)
                {
                    oldest = i;
                    time = channels[i].keyOnTime;
                }

                if (!channels[i].channel.isPlaying)
                {
                    channels[i].channel.clip = clip;
                    channels[i].channel.volume = volume;
                    channels[i].channel.pitch = pitch;
                    channels[i].channel.panStereo = pan;
                    channels[i].channel.loop = false;
                    channels[i].channel.Play();
                    channels[i].keyOnTime = Time.time;
                    return i;
                }
            }

            //运行到这里说明没有空闲频道。让新的音频顶替最早播出的音频
            if (oldest >= 0)
            {
                channels[oldest].channel.clip = clip;
                channels[oldest].channel.volume = volume;
                channels[oldest].channel.pitch = pitch;
                channels[oldest].channel.panStereo = pan;
                channels[oldest].channel.loop = false;
                channels[oldest].channel.Play();
                channels[oldest].keyOnTime = Time.time;
                return oldest;
            }

            return -1;
        }

        /// <summary>
        /// 循环播放，用于播放长时间的背景音乐，处理方式相对简单一些
        /// </summary>
        /// <param dataName="clip"></param>
        /// <param dataName="volume"></param>
        /// <param dataName="pan"></param>
        /// <param dataName="pitch"></param>
        /// <returns></returns>
        public int PlayLoop(AudioClip clip, float volume = 1.0f, float pan = 0.0f, float pitch = 1.0f)
        {
            Debug.Log(clip.name);
            for (var i = 0; i < channels.Length; i++)
                if (!channels[i].channel.isPlaying)
                {
                    channels[i].channel.clip = clip;
                    channels[i].channel.volume = volume;
                    channels[i].channel.pitch = pitch;
                    channels[i].channel.panStereo = pan;
                    channels[i].channel.loop = true;
                    channels[i].channel.Play();
                    channels[i].keyOnTime = Time.time;
                    return i;
                }

            return -1;
        }

        /// <summary>
        /// 停止所有音频
        /// </summary>
        public void StopAll()
        {
            foreach (var channel in channels)
                channel.channel.Stop();
        }

        /// <summary>
        /// 公开方法：根据频道ID停止音频
        /// </summary>
        /// <param dataName="id"></param>
        public void Stop(int id)
        {
            if (id >= 0 && id < channels.Length) channels[id].channel.Stop();
        }
    }
}