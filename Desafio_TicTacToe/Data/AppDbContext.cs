using Desafio_TicTacToe.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio_TicTacToe.Data
{

        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) :
           base(options)
            {
            }
            public DbSet<JogadorModel> Jogadores { get; set; }
            public DbSet<PartidaModel> Partidas { get; set; }
            public DbSet<JogadaModel> Jogadas { get; set; }
            public DbSet<LogModel> Logs { get; set; }
        }
}