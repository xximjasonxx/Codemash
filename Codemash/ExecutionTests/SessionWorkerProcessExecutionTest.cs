using Codemash.Poller.Container;
using Codemash.Poller.Process;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace ExecutionTests
{
    [TestClass]
    public class SessionWorkerProcessExecutionTest
    {
        [TestMethod]
        [Ignore]
        public void test_general_execution_of_session_worker_process()
        {
            var container = new PollerContainer();
            var process = container.Get<IProcess>("Session");
            process.Execute();
        }
    }
}
