using Cards.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Data {
    public class CardsDbContext : DbContext {
        public CardsDbContext(DbContextOptions options ): base(options) {

        }
        //'cards' is replica of database 'card' table
        public DbSet<Card> cards { get; set; }  //Card is Model name
    }
}
