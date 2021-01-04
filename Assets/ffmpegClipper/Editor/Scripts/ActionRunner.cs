using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

 
namespace ffmpegClipper
{
    [CreateAssetMenu(menuName = "ffmpeg Clipper/Action Runner")]
    public class ActionRunner : ScriptableObject
    {
        [System.Serializable]
        public class ActionItem
        {
            public string name;

            public Action action;
            public ConsoleAppManager appManager;

            public string interruptKey;
            public float delay = 0;

            ActionItem next;

            public System.Action<ActionItem> onProcessExited;
            public System.Action<ActionItem> onProcessStarted;

            bool isRunningPrev = false;


            public void Init(ActionItem next)
            {
                Debug.Log($"Init {action.name} next {next?.action?.name}");
                this.next = next;
                appManager = new ConsoleAppManager(name);
            }

            private void ProcessExited()
            {
                 Debug.Log("ProcessExited ");
                if (next != null)
                {
                    //Thread.Sleep((int)(delay/1000));
                    next.Start();
                }
                onProcessExited?.Invoke(this);
            }

            public void Update()
            {
                Debug.Log(">> Update " + action.name );
                if (isRunningPrev!= appManager.Running)
                {
                    Debug.Log(">> Change");
                    if (!appManager.Running)
                    {
                        Debug.Log(">> Finished");
                        ProcessExited();
                    }
                }
                isRunningPrev = appManager.Running;
            }

            public void Interrupt()
            {
                Debug.Log("StopCapture " );
                appManager.Write(interruptKey);
                //foreach (var l in action.ClipperListeners) l.OnInterrupt();
            }

            public void Start()
            {
                Debug.Log("Start ");
                string[] allArgs = new string[] { action.Args };
                //appManager.ProcessExited += ProcessExited;
                appManager.ExecuteAsync(allArgs);
                onProcessStarted?.Invoke(this);
                foreach (var l in action.ClipperListeners) l.OnStart();
            }
        }
        public bool isDebug;

        public List<ActionItem> actionItems = new List<ActionItem>();

        private int curItemNum = 0;

        public void Start()
        {
            int i = 0;
            foreach (var c in actionItems)
            {
                c.Init(i < actionItems.Count - 1 ? actionItems[i + 1] : null);
                // todo : proper unsubscribe
                c.onProcessExited -= OnProcessExited;
                c.onProcessExited += OnProcessExited;

                c.onProcessStarted -= OnProcessStarted;
                c.onProcessStarted += OnProcessStarted;
                i++;
            }

            EditorApplication.update -= OnUpdate;
            EditorApplication.update += OnUpdate;

            actionItems[0].Start();
        }

        private void OnProcessStarted(ActionItem obj)
        {
            curItemNum = actionItems.IndexOf(obj);
        }

        private void OnEnable()
        {
            
        }

        private void OnUpdate()
        {
            if (isDebug)
            {
                Debug.Log("Update");
            }
            actionItems[curItemNum].Update();
        }

        private void OnProcessExited(ActionItem obj)
        {
            if(actionItems.IndexOf(obj) == actionItems.Count - 1)
            {
                Debug.Log("OnRunnerComplete");
                foreach(var a in actionItems)
                {
                    foreach(var args in a.action.args)
                    {
                        if (args is IClipperListener listener) listener.OnRunnerComplete();
                        EditorApplication.update -= OnUpdate;
                    }
                }
            }
        }


        public void Interrupt()
        {
            actionItems[curItemNum].Interrupt();
            //EditorApplication.update -= OnUpdate;
        }
    }
}
