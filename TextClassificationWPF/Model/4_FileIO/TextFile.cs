using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextClassificationWPF.Foundation;

namespace TextClassificationWPF.FileIO
{
    public class TextFile:FileAdapter
    {
        string PROJECTPATH;

        const string FOLDERA = "ClassA";
        const string FOLDERB = "ClassB";
        public TextFile(params string[] fileType):base(fileType)
        {
            PROJECTPATH = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Classes\\";
        }
        public override List<string> GetAllFileNames(string folderName)
        {
            // Goes throug all types and saves the pathes
            List<string> fileNames = new List<string>();
            for (int i = 0; i < GetFileType().Count(); i++)
            {
                string[] paths = Directory.GetFiles(PROJECTPATH + folderName, "*." + GetFileType()[i]);
                foreach (string path in paths)
                {
                    fileNames.Add(path);
                }
            }
            return fileNames;
        }

        public string GetFilePathA(string fileName)
        {
            return @PROJECTPATH + FOLDERA + "\\" + fileName + "."+base.GetFileType();
        }

        public override string GetAllTextFromFileA(string path)
        {
            string text = string.Empty;
            // Tjeck if it is a PDF
            if (path.EndsWith(".pdf")) 
            {
                PdfReader reader = new PdfReader(path);
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    text += PdfTextExtractor.GetTextFromPage(reader, page);
                }
                reader.Close();
            }
            else
            {
                text = File.ReadAllText(path);
            }
            return text;
        }

        public override string GetAllTextFromFileB(string path)
        {
            string text = string.Empty;
            // Tjeck if it is a PDF
            if (path.EndsWith(".pdf"))
            {
                PdfReader reader = new PdfReader(path);
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    text += PdfTextExtractor.GetTextFromPage(reader, page);
                }
                reader.Close();
            }
            else
            {
                text = File.ReadAllText(path);
            }
            return text;
        }
    }
}
