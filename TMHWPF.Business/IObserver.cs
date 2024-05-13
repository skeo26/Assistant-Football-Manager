using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHWPF.Business
{
    public interface IObserver
    {
        void Update(string filePath, object data);
    }
}
