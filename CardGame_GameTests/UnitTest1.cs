using CardGame_DataAccess.DataInitializations;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CardGame_GameTests
{
    public class Tests
    {
        [Test]
        [Explicit]
        public async Task PopulateDatabase()
        {
            await Startup.CreateData();
        }
    }
}