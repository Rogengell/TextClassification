using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextClassificationWPF.FileIO
{
    public abstract class FileAdapter
    {
        private List<string> _fileType;
        public FileAdapter(params string[] fileType)
        {
            _fileType = new List<string>();
            for (int i = 0; i < fileType.Length; i++)
            {
                _fileType.Add(fileType[i].ToString());
            }
        }
        public abstract List<string> GetAllFileNames(string folderName);
        public abstract string GetAllTextFromFileA(string fileName);

        public abstract string GetAllTextFromFileB(string fileName);

        public List<string> GetFileType()
        {
            return _fileType;
        }
    }
}
