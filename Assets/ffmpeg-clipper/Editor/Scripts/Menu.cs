using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace ffmpegClipper
{
    class Menu : EditorWindow
    {
        private Rect upperPanel;
        private Rect resizer;

        private float sizeRatio = 0.5f;
        private bool isResizing;

        private GUIStyle resizerStyle;

        private static Settings settings;

        [MenuItem("ffmpegClipper/Settings")]
        private static void Settings()
        {
            Menu window = GetWindow<Menu>();
            window.titleContent = new GUIContent("ffmpeg Clipper");
        }

        [MenuItem("ffmpegClipper/Debug Args Readable")]
        private static void DebugArgsReadable()
        {
            IntPtr ptr = new IntPtr(256);

            FindOrCreateSettings();
            if (settings != null) Debug.Log($"Start : {settings.DebugArgs.ToString()}");
        }

        [MenuItem("ffmpegClipper/Debug Args")]
        private static void DebugArgs()
        {
            IntPtr ptr = new IntPtr(256);

            FindOrCreateSettings();
            if (settings != null) Debug.Log($"Start : {settings.StartArgs.ToString()}");
        }



        [MenuItem("ffmpegClipper/Start")]
        private static void Start()
        {
            FindOrCreateSettings();

            if (settings!=null) Debug.Log($"Start : {settings.name}");
            settings.StartCapture();

           
        }

        [MenuItem("ffmpegClipper/Stop")]
        private static void Stop()
        {
            if (settings != null) Debug.Log($"Stop : {settings.name}");
            settings.StopCapture();
        }


        private static void FindOrCreateSettings()
        {
            if (settings != null) return;

            string[] guids;
            guids = AssetDatabase.FindAssets("t:ffmpegClipper.Settings");
            foreach (string guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                settings = (Settings) AssetDatabase.LoadAssetAtPath(path, typeof(Settings));
                Debug.Log($"found : {settings.name}");
            }

            if (settings != null) return;

            settings = (Settings)ScriptableObject.CreateInstance(typeof(Settings));
            AssetDatabase.CreateAsset(settings, "Assets/ffmpegClipper/settings.asset");
            Debug.Log($"create new settings : {settings.name}");
        }

        private void OnEnable()
        {
            resizerStyle = new GUIStyle();
            resizerStyle.normal.background = EditorGUIUtility.Load("icons/d_AvatarBlendBackground.png") as Texture2D;
            FindOrCreateSettings();
        }

        private void OnGUI()
        {
            DrawUpperPanel();;
        }

        private void DrawUpperPanel()
        {
            upperPanel = new Rect(0, 0, position.width, position.height * sizeRatio);

            GUILayout.BeginArea(upperPanel);
            GUILayout.Label("Upper Panel");
            GUILayout.EndArea();
        }
    }
}
