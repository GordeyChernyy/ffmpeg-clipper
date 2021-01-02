using UnityEngine;

namespace ffmpegClipper
{
    [CreateAssetMenu(menuName = "ffmpeg Clipper/FrameRate")]
    class FrameRate : ClipperArgs
    {
        public int fps = 24;
        public override string Args => $"-framerate {fps}";
    }
}
