
using UnityEngine;

namespace ffmpegClipper
{
    abstract class ClipperArgs : ScriptableObject
    {
        public abstract string Args { get; }
    }
}
