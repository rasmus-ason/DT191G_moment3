using Dt191G_moment3.Models;
using Microsoft.EntityFrameworkCore;

namespace Dt191G_moment3.Data {
    public class MusicContext : DbContext {

        //Constructor
        public MusicContext(DbContextOptions<MusicContext>options) : base(options){

             Music = Set<Music>();

        }

        public DbSet<Music> Music {get; set;}

    }
}