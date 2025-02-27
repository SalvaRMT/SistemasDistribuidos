using Microsoft.EntityFrameworkCore;
using PokemonApi.Infrastructure.Entities; 
namespace PokemonApi.Infrastructure
{
    public class RelationalDbContext : DbContext
    {
        public RelationalDbContext(DbContextOptions<RelationalDbContext> options)
            : base(options)
        {
        }

        public DbSet<PokemonEntity> Pokemons { get; set; }
        public DbSet<HobbiesEntity> Hobbies { get; set; }
                public object Pokemon { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PokemonEntity>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Type).IsRequired().HasMaxLength(50);
                entity.Property(s => s.Level).IsRequired();
                entity.Property(s => s.Attack).IsRequired();
                entity.Property(s => s.Speed).IsRequired();
                entity.Property(s => s.weitgh);
            });

                        modelBuilder.Entity<HobbiesEntity>(entity =>{
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Top).IsRequired();
            });
        }
    }
}

