using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMHWPF.Business;

namespace TestsBusinessLogic
{
    public class ObserverManangerTest
    {
        [Fact]
        public void AddsObserverToObserversList()
        {
            // Arrange
            var observer = new TestObserver();
            var observerManager = new ObserverMananger();

            // Act
            observerManager.Attach(observer);

            // Assert
            Assert.Contains(observer, observerManager.Observers);
        }

        [Fact]
        public void RemovesObserverFromObserversList()
        {
            // Arrange
            var observer = new TestObserver();
            var observerManager = new ObserverMananger();
            observerManager.Attach(observer);

            // Act
            observerManager.Detach(observer);

            // Assert
            Assert.DoesNotContain(observer, observerManager.Observers);
        }

        [Fact]
        public void CallsUpdateOnEachObserver()
        {
            // Arrange
            var observer1 = new TestObserver();
            var observer2 = new TestObserver();
            var observerManager = new ObserverMananger();
            observerManager.Attach(observer1);
            observerManager.Attach(observer2);
            var data = new { Message = "Оповещение всех обсёрверов" };
            var filePath = "testfile.txt";

            // Act
            observerManager.Notify(filePath, data);

            // Assert
            Assert.True(observer1.WasUpdated);
            Assert.Equal(filePath, observer1.LastFilePath);
            Assert.Equal(data, observer1.LastData);
            Assert.True(observer2.WasUpdated);
            Assert.Equal(filePath, observer2.LastFilePath);
            Assert.Equal(data, observer2.LastData);
        }
    }
}
