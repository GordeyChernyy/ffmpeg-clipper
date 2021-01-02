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
        [System.Serializable]
        public class Console
        {
            public string name;

            public Settings settings;
            public ConsoleAppManager appManager;

            public string exit;

            Console next;

            public void Init(Console next)
            {
                this.next = next;
                appManager = new ConsoleAppManager(name);
            }

            private void ProcessExited(object sender, EventArgs e)
            {
                Debug.Log("ProcessExited " + name);
                if (next!=null) next.StartCapture();
            }

            public void StopCapture()
            {
                Debug.Log("StopCapture " + name);
                appManager.Write(exit);
                foreach (var l in settings.ClipperListeners) l.OnStopCapture();
            }

            public void StartCapture()
            {
                Debug.Log("StartCapture " + name);
                string[] allArgs = new string[] { settings.Args };
                appManager.ExecuteAsync(allArgs);
                appManager.ProcessExited += ProcessExited;
                foreach (var l in settings.ClipperListeners) l.OnStartCapture();
            }
        }

        public List<Console> consoles = new List<Console>();

        
        public Clipper()
        {
           
        }

        public void StartCapture()
        {
            int i = 0;
            foreach(var c in consoles)
            {
                c.Init(i < consoles.Count -1 ? consoles[i+1] : null);
                i++;
            }
            consoles[0].StartCapture();
        }

        public void StopCapture()
        {
            consoles[0].StopCapture();
        }

    }
}
