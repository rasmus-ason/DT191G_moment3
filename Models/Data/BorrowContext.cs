using Dt191G_moment3.Models;
using Microsoft.EntityFrameworkCore;

namespace Dt191G_moment3.Data {
    public class BorrowContext : DbContext {

        //Constructor
        public BorrowContext(DbContextOptions<BorrowContext>options) : base(options){

            Borrow = Set<Borrow>();

        }

        public DbSet<Borrow> Borrow {get; set;}
        
    }
}