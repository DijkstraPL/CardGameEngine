using CardGame_DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame_Server.Controllers
{
    [Route("api/decks")]
    [ApiController]
    public class DecksController : ControllerBase
    {
        IDeckRepository _deckRepository;
        public DecksController(IDeckRepository deckRepository)
        {
            _deckRepository = deckRepository ?? throw new ArgumentNullException(nameof(deckRepository));
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetNames()
        {
            var decks = await _deckRepository.GetDecks();
            return decks.Select(d => d.Name);
        }
    }
}
