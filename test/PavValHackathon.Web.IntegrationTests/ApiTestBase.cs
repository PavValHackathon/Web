using System;
using GodelTech.StoryLine.Wiremock;
using Xunit;

namespace PavValHackathon.Web.IntegrationTests
{
    [Collection(nameof(StartUpFixture))]
    public abstract class ApiTestBase : IDisposable
    {
        protected const int DefaultUserId = 1;
        
        protected ApiTestBase()
        {
            Config.ResetAll();
        }

        public virtual void Dispose()
        {
            Config.ResetAll();
        }
    }
}