using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PintTests
{
    public class TempFile : IDisposable
    {
        public string FileName { get; private set; }
        public TempFile(string content)
        {
            FileName = Path.GetTempFileName();
            File.WriteAllText(FileName, content);
        }

        public void Dispose()
        {
            try
            {
                File.Delete(FileName);
            }
            catch (IOException)
            {
            }
        }
    }
}
