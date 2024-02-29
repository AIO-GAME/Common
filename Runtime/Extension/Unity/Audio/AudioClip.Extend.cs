using UnityEngine;

namespace AIO.UEngine
{
    public static class AudioClipExtend
    {
        /// <summary>
        /// AudioClip转换成字节数组
        /// </summary>
        /// <returns> 压缩过的byte </returns>
        public static byte[] AudioToBytes(this AudioClip clip, int lastPosition)
        {
            var data = new float[lastPosition * clip.channels * 4];
            clip.GetData(data, 0);
            var bytes = new byte[data.Length * 4];
            System.Buffer.BlockCopy(data, 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// 字节数组转换为AudioClip
        /// </summary>
        public static AudioClip BytesToAudio(this AudioClip clip, byte[] bytes)
        {
            var data = new float[bytes.Length / 4];
            System.Buffer.BlockCopy(bytes, 0, data, 0, data.Length);
            clip.SetData(data, 0);
            return clip;
        }
    }
}