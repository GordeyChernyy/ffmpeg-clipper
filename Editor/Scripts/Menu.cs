using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace ffmpegClipper
{
    static class Menu
    {
        private static Clipper clipper;

        [MenuItem("ffmpegClipper/Settings")]
        private static void Settings()
        {
            FindOrCreateSettings();
            EditorGUIUtility.PingObject(clipper);
            Selection.activeObject = clipper;
        }

        [MenuItem("ffmpegClipper/Debug Args Readable")]
        private static void DebugArgsReadable()
        {
            IntPtr ptr = new IntPtr(256);

            FindOrCreateSettings();
            if (clipper != null) Debug.Log($"{clipper.DebugArgs.ToString()}");
        }

        [MenuItem("ffmpegClipper/Debug Args")]
        private static void DebugArgs()
        {
            IntPtr ptr = new IntPtr(256);

            FindOrCreateSettings();
            if (clipper != null) Debug.Log($"{clipper.Args.ToString()}");
        }

        [MenuItem("ffmpegClipper/Start")]
        private static void Start()
        {
            FindOrCreateSettings();

            if (clipper!=null) Debug.Log($"Start : {clipper.name}");
            clipper.Start();
        }

        [MenuItem("ffmpegClipper/Stop")]
        private static void Stop()
        {
            if (clipper != null) Debug.Log($"Stop : {clipper.name}");
            clipper.Interrupt();
        }

        [MenuItem("ffmpegClipper/GitHub")]
        private static void GoToGitHub()
        {
            Application.OpenURL("https://github.com/GordeyChernyy/ffmpeg-clipper");
        }

        private static void FindOrCreateSettings()
        {
            if (clipper != null) return;

            string[] guids;
            guids = AssetDatabase.FindAssets("t:ffmpegClipper.Clipper");
            foreach (string guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                clipper = (Clipper) AssetDatabase.LoadAssetAtPath(path, typeof(Clipper));
                Debug.Log($"found : {clipper.name}");
            }

            if (clipper != null) return;

            clipper = (Clipper)ScriptableObject.CreateInstance(typeof(Clipper));
            AssetDatabase.CreateAsset(clipper, "Assets/ffmpegClipper/ffmpegClipper.asset");
            Debug.Log($"create new settings : {clipper.name}");
        }
    }
}
