using UnityEngine;

namespace ffmpegClipper
{
    [CreateAssetMenu(menuName = "ffmpeg Clipper/CustomArgs")]
    class CustomArgs : ClipperArgs
    {
        public string args;
        public override string Args => args;
    }
}
