using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMHWPF.Business
{
    public class ObserverMananger : IObservable
    {
        private List<IObserver> observers = new List<IObserver>();
        public IReadOnlyList<IObserver> Observers => observers.AsReadOnly();

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(string filePath, object data)
        {
            foreach (var observer in observers)
            {
                observer.Update(filePath, data);
            }
        }
    }
}
