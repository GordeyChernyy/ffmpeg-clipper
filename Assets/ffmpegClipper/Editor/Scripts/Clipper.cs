using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEditor;
using UnityEngine;

using Debug = UnityEngine.Debug;

namespace ffmpegClipper
{
    public class Clipper : ScriptableObject
    {
        public Settings settings;

        ConsoleAppManager appManager;

        public Clipper()
        {
            appManager = new ConsoleAppManager("ffmpeg");
        }

        public void StartCapture()
        {
            StartCapture(settings.StartArgs, settings.ClipperListeners);
        }

        public void StopCapture()
        {
            StopCapture(settings.StopArgs, settings.ClipperListeners);
        }

        public void StopCapture(string args, List<IClipperListener> listeners)
        {
            appManager.Write("q");
            Thread.Sleep(2000);
            foreach (var l in listeners) l.OnStopCapture();
        }

        public void StartCapture(string args, List<IClipperListener> listeners)
        {
            string[] allArgs = new string[] {args};
            appManager.ExecuteAsync(allArgs);
            foreach (var l in listeners) l.OnStartCapture();
        }
    }
}
