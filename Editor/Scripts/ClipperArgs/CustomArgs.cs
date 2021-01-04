using ScriptableVariables;
using System;
using UnityEngine;

namespace ffmpegClipper
{
    [CreateAssetMenu(menuName = "ffmpeg Clipper/CustomArgs")]
    class CustomArgs : ClipperArgs
    {
        public SString[] variables;

        public string args;
        public override string Args => ArgsWithVariables();

        [ContextMenu("Verify")]
        public void Verify()
        {
            Debug.Log(Args);
        }

        private string ArgsWithVariables()
        {
            var result = args.Split(new[] { "{", "}" }, StringSplitOptions.None);

            int i = 0;
            string output = "";
            foreach (var r in result)
            {
                if (i % 2 != 0)
                {
                    int.TryParse(r, out int index);
                    output += variables[index].value;
                }
                else
                {
                    output += r;
                }
                i++;
            }
            return output;
        }
    }
}
