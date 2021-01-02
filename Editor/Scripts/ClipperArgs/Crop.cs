
using UnityEditor;
using UnityEngine;

namespace ffmpegClipper
{
    [CreateAssetMenu(menuName = "ffmpeg Clipper/Window Settings")]
    class Crop : ClipperArgs
    {
        public enum Window { Game, Scene, Project}


        public Vector2 posOffset;
        public Vector2 sizeOffset;
        public Window window;

        private static Rect windowRect;

        public override string Args {
            get{
                string w = "";

                if (window == Window.Game) w = "UnityEditor.GameView,UnityEditor";
                if (window == Window.Scene) w = "UnityEditor.SceneView,UnityEditor";
                if (window == Window.Project) w = "UnityEditor.ProjectBrowser,UnityEditor";

                System.Type T = System.Type.GetType(w);
                EditorWindow gameview = EditorWindow.GetWindow(T);
                windowRect = gameview.position;

                string args = "";

                int width = (int) (windowRect.size.x + sizeOffset.x);
                int height = (int) (windowRect.size.y + sizeOffset.y);

                args = $"-filter:v \"crop={(width/2)*2}:{(height/2)*2}:{windowRect.xMin + posOffset.x}:{windowRect.yMin + posOffset.y}\"";

                return args;
            }
        }
    }
}
