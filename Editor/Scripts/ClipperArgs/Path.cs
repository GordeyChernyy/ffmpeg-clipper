using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ffmpegClipper {
    [CreateAssetMenu(menuName = "ffmpeg Clipper/Path")]
    class Path : ClipperArgs, IClipperListener
    {
        [System.Serializable]
        public class TrackedPathData
        {
            public string name;
            public int majorVersion = 0;
            public int minorVersion = 0;

            public void IncrementVersion()
            {
                minorVersion++;
            }

            public void IncrementMajorVersion()
            {
                majorVersion++;
                minorVersion = 0;
            }
        }

        public string CurFileName;

        public List<TrackedPathData> pathData = new List<TrackedPathData>();

        public enum VideoFormat { mpg, mkv, mp4 }

        public string path = "/../ScreenCaptures";
        public VideoFormat videoFormat;


        TrackedPathData CurData
        {
            get
            {
                var fileName = CurFileName;

                var data = pathData.Find(obj => obj.name == fileName);
                if (data != null) return data;

                data = new TrackedPathData() { name = fileName };
                pathData.Add(data);
                return data;
            }
        }

        [ContextMenu("Increment Major Version")]
        public void IncrementMajorVersion()
        {
            CurData.IncrementMajorVersion();
            EditorUtility.SetDirty(this);
        }

        public void IncrementVersion()
        {
            CurData.IncrementVersion();
            EditorUtility.SetDirty(this);
        }

        public void OnStartCapture()
        {
            
        }

        public void OnStopCapture()
        {
            Application.OpenURL(Args);
            IncrementVersion();
        }

        public override string Args {
            get
            {
                var stringPath = Application.dataPath + path;

                try
                {
                    if (!Directory.Exists(stringPath))
                    {
                        Directory.CreateDirectory(stringPath);
                    }
                }
                catch (IOException ex)
                {
                    Debug.Log(ex.Message);
                }

                string fullPath = System.IO.Path.GetFullPath(stringPath);

           
                return $"{fullPath}\\{CurData.name}_v{CurData.majorVersion}.{CurData.minorVersion}.{videoFormat.ToString()}";
            }
        }

    }
}
