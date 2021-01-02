using System;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace ffmpegClipper
{
    [CreateAssetMenu(menuName = "ffmpeg Clipper/Audio")]
    class Audio : ClipperArgs
    {
        public string deviceName;
        public string args;
        public override string Args => $"-f dshow -i audio=\"{deviceName}\"";

        [ContextMenu("PrintDevices")]
        public void PrintDevices()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = "cmd",
                    UseShellExecute = true,
                    CreateNoWindow = false,
                    Arguments = args
                };
                Process myProcess = new Process
                {
                    StartInfo = startInfo
                };

                myProcess.Start();
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}
