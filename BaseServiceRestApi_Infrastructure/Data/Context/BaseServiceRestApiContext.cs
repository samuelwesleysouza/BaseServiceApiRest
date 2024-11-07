using BaseServiceApiRest_Core.Entities;  // Certifique-se de que as entidades estão no namespace correto
using Microsoft.EntityFrameworkCore;
using System;

namespace BaseServiceRestApi_Infrastructure.Data.Context
{
    public class BaseServiceRestApiContext : DbContext
    {
        public BaseServiceRestApiContext(DbContextOptions<BaseServiceRestApiContext> options) : base(options)
        {
        }

        // Definindo as DbSets que representam as tabelas no banco de dados
        public DbSet<HelpFriend> HelpFriends { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Users> Users { get; set; }

        // Configurações adicionais para as tabelas, caso necessário
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  // Chama o método base para garantir que qualquer configuração adicional seja aplicada

            // Definindo as tabelas no banco de dados
            modelBuilder.Entity<HelpFriend>().ToTable("HelpFriends");
            modelBuilder.Entity<Manager>().ToTable("Managers");
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<School>().ToTable("Schools");
            modelBuilder.Entity<Users>().ToTable("Users");
        }
    }
}
