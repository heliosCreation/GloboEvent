using GloboEvent.Api.IntegrationTest.Base;
using System.Threading.Tasks;

namespace GloboEvent.Api.IntegrationTest.Controllers.Events.Queries
{
    public class EventController_QueriesTest : IntegrationTestBase
    {
        #region GetALL
        public async Task GetAll_ReturnsAllSeededEvents()
        {
            await AuthenticateAsync();

        }
        #endregion
    }
}
