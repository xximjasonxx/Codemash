using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Ex;
using Codemash.Api.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Server.CoreTests.Factory;

namespace Server.CoreTests
{
    [TestClass]
    public class LoadedSessionRepositoryTests
    {
        private ISessionRepository _sessionRepository;

        [TestInitialize]
        public void Init()
        {
            var container = new StandardKernel();
            container.Bind<ISessionRepository>().ToConstant(MoqSessionRepositoryFactory.GetSessionRepositoryMock());
            _sessionRepository = container.Get<ISessionRepository>();
        }
    }
}
