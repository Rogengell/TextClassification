using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TextClassificationWPF.Domain
{
    public class FileLists
    {
        private List<string> _b;

        private List<string> _a;


        public FileLists(){

        }

        public void SetA(List<string> a)
        {
            _a = a;
        }

        public void SetB(List<string> b)
        {
            _b = b;
        }

        public List<string> GetA()
        {
            return _a;
        }

        public List<string> GetB()
        {
            return _b;
        }

        public List<string> GetBoth()
        {
            return _a.Concat(_b).ToList(); ;
        }
    }
}
