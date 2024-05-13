using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMHWPF.Business;

namespace TestsBusinessLogic
{
    public class TestObserver : IObserver
    {
        public string LastFilePath { get; private set; }
        public object LastData { get; private set; }
        public bool WasUpdated { get; private set; } = false;

        public void Update(string filePath, object data)
        {
            LastFilePath = filePath;
            LastData = data;
            WasUpdated = true;
        }

        public void Reset()
        {
            LastFilePath = null;
            LastData = null;
            WasUpdated = false;
        }
    }
}
