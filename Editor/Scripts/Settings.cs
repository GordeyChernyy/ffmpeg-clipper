using System.Collections.Generic;
using UnityEngine;

namespace ffmpegClipper
{
    [CreateAssetMenu(menuName = "ffmpeg Clipper/Settings")]
    public class Settings : ScriptableObject
    {
        [System.Serializable]
        public class ArgsData
        {
            public List<ClipperArgs> data;
        }

        public List<ArgsData> passes = new List<ArgsData>();

        public string DebugArgs
        {
            get
            {
                string result = "";

                foreach (var pass in passes)
                {
                    foreach(var data in pass.data)
                    {
                        result += data.Args + "\n";
                    }
                    result += "&& ";
                }

                return result;
            }
        }

        public string Args
        {
            get
            {
                string result = "";

                foreach (var pass in passes)
                {
                    foreach (var data in pass.data)
                    {
                        result += data.Args + " ";
                    }
                }
                return result;
            }
        }


        public List<IClipperListener> ClipperListeners
        {
            get
            {
                List<IClipperListener> listeners = new List<IClipperListener>();
                foreach (var pass in passes)
                {
                    foreach(var data in pass.data)
                    {
                        if (data is IClipperListener listener)
                        {
                            listeners.Add(listener);
                        }
                    }
                }
                return listeners;
            }
        }
    }
}
