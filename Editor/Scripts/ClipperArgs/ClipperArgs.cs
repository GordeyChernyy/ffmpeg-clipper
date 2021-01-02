
using UnityEngine;

namespace ffmpegClipper
{
    public abstract class ClipperArgs : ScriptableObject
    {
        public abstract string Args { get; }
    }
}
