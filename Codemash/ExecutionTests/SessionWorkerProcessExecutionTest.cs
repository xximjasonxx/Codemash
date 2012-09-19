
using System.Transactions;
using Codemash.Poller.Container;
using Codemash.Poller.Process;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace ExecutionTests
{
    [TestClass]
    public class SessionWorkerProcessExecutionTest
    {
        private TransactionScope _transactionScope;

        [TestInitialize]
        public void Initialize()
        {
            _transactionScope = new TransactionScope();
        }

        [TestMethod]
        [Ignore]
        public void test_general_execution_of_session_worker_process()
        {
            var container = new PollerContainer();
            var process = container.Get<IProcess>("Session");
            process.Execute();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _transactionScope.Dispose();
        }
    }
}
