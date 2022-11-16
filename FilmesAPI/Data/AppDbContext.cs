using FilmesApi.Models;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class AppDbContext : DbContext
    {
        //Define como parametro do construtor padrão uma 
        //variavel opt do tipo DbContextOptions<FilmeContext> e passa para 
        //A classe pai DbContext
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            // Relação 1:1
            builder.Entity<Endereco>()
                .HasOne(endereco => endereco.Cinema) //Define que nossa entidade Endereco tem um cinema
                .WithOne(cinema => cinema.Endereco) //Definimos que um cinema tbm possui um endereço
                .HasForeignKey<Cinema>(cinema => cinema.EnderecoId); //Definimos que nossa chave estrangeira está em cinema e é o nosso EndereçoId

            // Relação 1:n
            builder.Entity<Cinema>()
                .HasOne(cinema => cinema.Gerente) //Diz que cada cinema terá um gerente
                .WithMany(gerente => gerente.Cinemas) //Diz que um gerente pode ter varios cinemas
                .HasForeignKey(cinema => cinema.GerenteId);//Definimos que nossa chave estrangeira está em cinema e é o nosso GerenteId 

            // Relação n:n 
            builder.Entity<Sessao>()
                .HasOne(sessao => sessao.Filme)//Diz que cada cinema terá um Filme
                .WithMany(filme => filme.Sessoes) //Diz que cada filme terá varias sessões
                .HasForeignKey(sessao => sessao.FilmeId);//Definimos que nossa chave estrangeira está em sessao e é o nosso FilmeId 

            builder.Entity<Sessao>()
                .HasOne(sessao => sessao.Cinema)//Diz que cada cinema terá um Cinema
                .WithMany(cinema => cinema.Sessoes)//Diz que cada cinema terá varias sessões
                .HasForeignKey(sessao => sessao.CinemaId);//Definimos que nossa chave estrangeira está em sessao e é o nosso CinemaId 
        }

        //DbSet é o conjunto de dados do banco que nos permite acessar esses dados
        //Ele recebe um objeto que é o qual queremos mapear e acessar, no caso, um Filme
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Gerente> Gerentes { get; internal set; }
        public DbSet<Sessao> Sessoes { get; internal set; }

    }
}
