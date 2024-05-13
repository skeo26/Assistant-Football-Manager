using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHWPF.Business
{
    public interface IObservable
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(string filePath, object data);
    }
}
