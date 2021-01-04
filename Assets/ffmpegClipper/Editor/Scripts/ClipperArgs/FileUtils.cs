using ScriptableVariables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ffmpegClipper
{
    [CreateAssetMenu(menuName = "ffmpeg Clipper/File Utils")]
    public class FileUtils : ClipperArgs
    {
        public SString folder;
        public SString file;

        public enum FileType { latest}

        public FileType fileType;

        public override string Args {
            get
            {
                var sortedFiles = new DirectoryInfo(folder.value).GetFiles()
                                                  .OrderByDescending(f => f.LastWriteTime)
                                                  .ToList();
              
                sortedFiles.RemoveAll(obj=>obj.Extension!=".mp4");
                Debug.Log(sortedFiles[0].Extension);
                var result = sortedFiles[0];
                return $"\"{result.FullName}\"";
            }
        }
    }
}
