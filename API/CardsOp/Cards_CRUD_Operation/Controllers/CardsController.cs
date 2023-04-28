using Cards.API.Data;
using Cards.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cards_CRUD_Operation.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller {
        private readonly CardsDbContext cardsDbContext;
        public CardsController(CardsDbContext cardsDbContext) {
            this.cardsDbContext = cardsDbContext;
        }

        //Get All Cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards(){
            var cards = await cardsDbContext.cards.ToListAsync();
            return Ok(cards);
        }

        //Get Single Card
        [HttpGet]
        [Route("{id:guid}")]
        //[ActionName("GetAllCards")]
        public async Task<IActionResult> GetAllCards([FromRoute] Guid id) {
            var cards = await cardsDbContext.cards.FirstOrDefaultAsync(x => x.Id == id);
            if(cards != null) { 
                return Ok(cards);
            } else {
                return NotFound("Card Not Found");
            }
        }
        //Get Single Card
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card) {
            card.Id = Guid.NewGuid();
            await cardsDbContext.cards.AddAsync(card);
            await cardsDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllCards), new {id = card.Id}, card);
        }
        //Updating Card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card) {
            var existingCard = await cardsDbContext.cards.FirstOrDefaultAsync(x => x.Id == id);
            if(existingCard != null) {
                existingCard.CardholderName = card.CardholderName;
                existingCard.CardNumber = card.CardNumber;
                existingCard.ExpiryMonth = card.ExpiryMonth;
                existingCard.ExpiryYear = card.ExpiryYear;
                existingCard.cvv = card.cvv;
                await cardsDbContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            return NotFound("Card Not Found");
        }
        //Delete Card
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id) {
            var existingCard = await cardsDbContext.cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null) {
                cardsDbContext.Remove(existingCard);
                await cardsDbContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            return NotFound("Card Not Found");
        }
    }
}
