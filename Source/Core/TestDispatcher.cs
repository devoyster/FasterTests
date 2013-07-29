using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Funt.Core.Models;

namespace Funt.Core
{
    public class TestDispatcher
    {
        public IEnumerable<TestResult> EnqueueTests(IEnumerable<TestDescriptor> tests)
        {
            var results = new ConcurrentQueue<TestResult>();
            var newResultAddedEvent = new ManualResetEventSlim(false);

            var dispatchTask = Task.Factory.StartNew(() => SplitAndRunTests(tests, results, newResultAddedEvent));

            do
            {
                newResultAddedEvent.Wait(1000);

                TestResult result;
                while (results.TryDequeue(out result))
                {
                    yield return result;
                }

                newResultAddedEvent.Reset();
            } while (!dispatchTask.IsCompleted);
        }

        private void SplitAndRunTests(IEnumerable<TestDescriptor> tests, ConcurrentQueue<TestResult> results, ManualResetEventSlim newResultAddedEvent)
        {
            foreach (var test in tests)
            {
                results.Enqueue(new TestResult
                                    {
                                        Test = test,
                                        IsSuccess = true
                                    });

                newResultAddedEvent.Set();
            }
        }
    }
}