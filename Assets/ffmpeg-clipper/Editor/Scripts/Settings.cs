﻿using System.Collections.Generic;
using UnityEngine;

namespace ffmpegClipper
{
    [CreateAssetMenu(menuName = "ffmpeg Clipper/Settings")]
    class Settings : ScriptableObject
    {
        public List<ClipperArgs> data = new List<ClipperArgs>();

        Clipper clipper = new Clipper();

        public void StartCapture()
        {
            clipper.StartCapture(StartArgs, ClipperListeners);
        }

        public void StopCapture()
        {
            clipper.StopCapture(StopArgs, ClipperListeners);
        }

        public string DebugArgs
        {
            get
            {
                string result = "";

                foreach (var d in data)
                {
                    result += d.Args + "\n";
                }

                return result;
            }
        }

        public string StartArgs
        {
            get
            {
                string result = "";

                foreach (var d in data)
                {
                    result += d.Args + " ";
                }

                return result;
            }
        }

        public string StopArgs
        {
            get
            {
                string result = "";

                foreach (var d in data)
                {
                    if(d is Path path)
                    {
                        result = d.Args;
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
                foreach (var d in data)
                {
                    if (d is IClipperListener listener)
                    {
                        listeners.Add(listener);
                    }
                }
                return listeners;
            }
        }
    }
}
