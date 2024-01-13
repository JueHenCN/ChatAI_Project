using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAI_Connect.Python
{
    public class PythonProgram
    {
        public string PythonPath { get; set; }
        public string PythonScriptPath { get; set; }

        public void Run()
        {
            System.Diagnostics.Process.Start(PythonPath, PythonScriptPath);
        }
    }
}
