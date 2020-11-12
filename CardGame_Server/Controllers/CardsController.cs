using CardGame_DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame_Server.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;

        public CardsController(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
        }

        [HttpGet]
        public async Task<IEnumerable<CardGame_Data.Data.Card>> GetCards()
        {
            var cards = await _cardRepository.GetCards();
            return cards.Select(c =>(CardGame_Data.Data.Card)c);
        }
    }
}
