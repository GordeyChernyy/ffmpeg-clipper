using System.Collections.Generic;
using System.IO;
using System.Threading;
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

        private string _filePath;
        public string FilePath => _filePath;

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
            Debug.Log("IncrementVersion");
            CurData.IncrementVersion();
            EditorUtility.SetDirty(this);
        }

        public void OnStart()
        {
            
        }

        public void OnInterrupt()
        {
            
        }

        public void OnRunnerComplete()
        {
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

                _filePath = $"{fullPath}\\{CurData.name}_v{CurData.majorVersion}.{CurData.minorVersion}.{videoFormat.ToString()}";

                return _filePath;
            }
        }

    }
}
