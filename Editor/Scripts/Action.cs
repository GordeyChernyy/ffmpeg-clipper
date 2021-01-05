using System.Collections.Generic;
using UnityEngine;

namespace ffmpegClipper
{
    [CreateAssetMenu(menuName = "ffmpeg Clipper/Action")]
    public class Action : ScriptableObject
    {

        public List<ClipperArgs> args = new List<ClipperArgs>();

        public string DebugArgs
        {
            get
            {
                string result = "";

                foreach (var arg in args)
                {
                    result += arg.Args + "\n";
                }
                return result;
            }
        }

        public string Args
        {
            get
            {
                string result = "";

                foreach (var arg in args)
                {
                    result += arg.Args + " ";
                }
                return result;
            }
        }

        [ContextMenu("DebugArgs")]
        public void PrintArgs()
        {
            Debug.Log(Args);
        }

        public List<IClipperListener> ClipperListeners
        {
            get
            {
                List<IClipperListener> listeners = new List<IClipperListener>();
                foreach (var arg in args)
                {
                    if (arg is IClipperListener listener)
                    {
                        listeners.Add(listener);
                    }
                }
                return listeners;
            }
        }
    }
}
