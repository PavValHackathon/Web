using Xunit;

namespace PavValHackathon.Web.IntegrationTests
{
    [CollectionDefinition(nameof(StartUpFixture))]
    public sealed class StartUpFixtureCollection : ICollectionFixture<StartUpFixture>
    {
    }
}