using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ffmpegClipper
{
    [CreateAssetMenu(menuName = "ffmpeg Clipper/AppVersion")]
    class AppVersion : ClipperArgs
    {
        public override string Args => PlayerSettings.bundleVersion;
    }
}
