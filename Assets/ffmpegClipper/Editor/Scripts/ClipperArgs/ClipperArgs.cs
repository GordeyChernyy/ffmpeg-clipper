﻿
using UnityEngine;

namespace ffmpegClipper
{
    [System.Serializable]
    public abstract class ClipperArgs : ScriptableObject
    {
        public abstract string Args { get; }

        [ContextMenu("PrintArgs")]
        public virtual void PrintArgs() {
            Debug.Log(Args);
        }
    }
}
