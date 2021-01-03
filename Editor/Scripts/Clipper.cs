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
        public ActionRunner actionRunner;

        internal void Start()
        {
            actionRunner.Start();
        }

        internal void Interrupt()
        {
            actionRunner.Interrupt();
        }

        public string Args
        {
            get {
                string args = "";
                foreach (var a in actionRunner.actionItems)
                {
                    args += a.action.Args;
                }
                return args;
            }
        }

        public string DebugArgs
        {
            get
            {
                string args = "";
                foreach (var a in actionRunner.actionItems)
                {
                    args += a.action.DebugArgs;
                }
                return args;
            }
        }
    }
}
