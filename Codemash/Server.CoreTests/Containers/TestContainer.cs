using Codemash.Api.Data.Parsing;
using Codemash.Api.Data.Parsing.Impl;
using Ninject;
using Server.CoreTests.Factory;

namespace Server.CoreTests.Containers
{
    internal class TestContainer : StandardKernel
    {
        internal TestContainer()
        {
            // test data factory
            Bind<TestDataFactory>().ToSelf();

            // entity parsing
            Bind<ISessionEntityParser>().To<CodemashSessionEntityParser>();
            Bind<ISpeakerEntityParser>().To<CodemashSpeakerEntityParser>();
        }
    }
}
